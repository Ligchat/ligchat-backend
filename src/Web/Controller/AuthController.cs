using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text;
using LigChat.Backend.Application.Interface.UserInterface;
using LigChat.Backend.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LigChat.Backend.Web.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;
        private readonly IUserServiceInterface _userService;

        public AuthController(JwtTokenService jwtTokenService, IConfiguration configuration, IUserServiceInterface userService)
        {
            _jwtTokenService = jwtTokenService;
            _configuration = configuration;
            _userService = userService;
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("Smtp");

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Host = smtpSettings["Host"];
                    smtpClient.Port = int.Parse(smtpSettings["Port"]);
                    smtpClient.Credentials = new NetworkCredential(smtpSettings["User"], smtpSettings["Password"]);
                    smtpClient.EnableSsl = true;

                    using (var mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(smtpSettings["FromEmail"], smtpSettings["FromName"]);
                        mailMessage.To.Add(toEmail);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;

                        // Adiciona a logo como anexo
                     //   var logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src/Assets", "Logo.png");

                     //   if (System.IO.File.Exists(logoPath)) 
                     //   {
                    //        var logoAttachment = new Attachment(logoPath);
                    //        logoAttachment.ContentId = "logo"; // Isso deve corresponder ao cid no HTML
                   //         mailMessage.Attachments.Add(logoAttachment);
                    //    }

                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"Erro ao enviar e-mail SMTP: {smtpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
                throw;
            }
        }

        [HttpGet("validate")]
        public IActionResult ValidateMagicLink([FromQuery] string token)
        {
            if (IsValidToken(token, out var userId))
            {
                return Ok(new { Message = "Token is valid", UserId = userId });
            }

            return Unauthorized();
        }

        private bool IsValidToken(string token, out int userId)
        {
            userId = 0;
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"]
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                userId = int.Parse(jwtToken.Claims.First(claim => claim.Type == "userId").Value);

                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost("send-verification-code")]
        public async Task<IActionResult> SendVerificationCode([FromBody] SendVerificationCodeRequestDTO request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("Email is required.");
            }

            var user = _userService.GetByEmail(request.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var verificationCode = GenerateRandomCode();
            if (user.Code == "200")
            {
                await _userService.SaveVerificationCode(user.Data.Id, verificationCode);

                // Carregar o template HTML
                string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SendCodeTemplate.html");

                string emailBody = await System.IO.File.ReadAllTextAsync(templatePath);

                // Substituir o código de verificação no template
                emailBody = emailBody.Replace("{{codigo_verificacao}}", verificationCode);
                // Aqui você pode definir o caminho da logo que será incluída no email
                string logoPath = "cid:logo"; // Isso será usado para referenciar a logo no email

                // Enviar o e-mail com o template
                await SendEmailAsync(request.Email, "Código de Verificação", emailBody);

                return Ok(new { Message = "Verification code sent." });
            }

            return NotFound(new { Message = "User Not Found." });
        }

        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequestDTO request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Code))
            {
                return BadRequest("Invalid request.");
            }

            var isValid = await _userService.ValidateVerificationCode(request.Email, request.Code);
            if (!isValid)
            {
                return Unauthorized("Invalid verification code.");
            }

            var user = _userService.GetByEmail(request.Email);
            var token = _jwtTokenService.GenerateToken(user.Data.Id.ToString(), request.Email);
            return Ok(new { Token = token });
        }

        private string GenerateRandomCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }

    public class SendMagicLinkRequest
    {
        public string Email { get; set; }
    }

    public class SendVerificationCodeRequestDTO
    {
        public string Email { get; set; }
    }

    public class VerifyCodeRequestDTO
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }

    public class VerifyCodeResponseDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
