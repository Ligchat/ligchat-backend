using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static tests_.src.Domain.DTOs.Whatsapp.Whatsapp;
using tests_.src.Application.Services;

namespace LigChat.Backend.Web.Controller
{
    [ApiController]
    [Route("api/whatsapp")]
    public class WhatsAppIntegrationController: ControllerBase
    {
        private readonly WhatsAppIntegrationService _whatsAppService;

        public WhatsAppIntegrationController(WhatsAppIntegrationService whatsAppService)
        {
            _whatsAppService = whatsAppService;
        }

        // POST: api/whatsapp/settings
        [HttpPost("settings")]
        public async Task<IActionResult> CreateSettings([FromBody] CreateWhatsAppIntegrationSettingsDTO dto)
        {
            var settings = await _whatsAppService.CreateSettingsAsync(dto);
            return Ok(settings);
        }

        // GET: api/whatsapp/settings/{id}
        [HttpGet("settings/{id}")]
        public async Task<IActionResult> GetSettings(int id)
        {
            var settings = await _whatsAppService.GetSettingsAsync(id);
            return Ok(settings);
        }

        // PUT: api/whatsapp/settings/{id}
        [HttpPut("settings/{id}")]
        public async Task<IActionResult> UpdateSettings(int id, [FromBody] UpdateWhatsAppIntegrationSettingsDTO dto)
        {
            var settings = await _whatsAppService.UpdateSettingsAsync(id, dto);
            return Ok(settings);
        }

        // DELETE: api/whatsapp/settings/{id}
        [HttpDelete("settings/{id}")]
        public async Task<IActionResult> DeleteSettings(int id)
        {
            await _whatsAppService.DeleteSettingsAsync(id);
            return NoContent();
        }

        // POST: api/whatsapp/send-message
        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage([FromBody] WhatsAppTextMessageDTO dto)
        {
            await _whatsAppService.SendTextMessageAsync(dto.RecipientPhone, dto.Text, dto.SettingsId);
            return Ok(new { message = "Mensagem enviada com sucesso" });
        }

        // POST: api/whatsapp/send-image
        [HttpPost("send-image")]
        public async Task<IActionResult> SendImage([FromBody] WhatsAppMediaMessageDTO dto)
        {
            await _whatsAppService.SendImageMessageAsync(dto.RecipientPhone, dto.MediaUrl, dto.Caption, dto.SettingsId);
            return Ok(new { message = "Imagem enviada com sucesso" });
        }

        // POST: api/whatsapp/send-document
        [HttpPost("send-document")]
        public async Task<IActionResult> SendDocument([FromBody] WhatsAppMediaMessageDTO dto)
        {
            await _whatsAppService.SendDocumentMessageAsync(dto.RecipientPhone, dto.MediaUrl, dto.FileName, dto.SettingsId);
            return Ok(new { message = "Documento enviado com sucesso" });
        }

        [HttpGet("messages/{connectionId}")]
        public async Task<IActionResult> GetMessages(int connectionId)
        {
            var messages = await _whatsAppService.GetMessagesByConnectionId(connectionId);
            return Ok(messages);
        }

        [HttpGet("account/{sessionId}")]
        public async Task<IActionResult> GetWhatsAppAccount(string sessionId)
        {
            var account = await _whatsAppService.GetWhatsAppAccount(sessionId);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        // POST: api/whatsapp/send-audio
        [HttpPost("send-audio")]
        public async Task<IActionResult> SendAudio([FromBody] WhatsAppMediaMessageDTO dto)
        {
            await _whatsAppService.SendAudioMessageAsync(dto.RecipientPhone, dto.MediaUrl, dto.SettingsId);
            return Ok(new { message = "Áudio enviado com sucesso" });
        }

        [HttpGet("verify-token")]
        public IActionResult VerifyWebhook([FromQuery] string hub_mode, [FromQuery] string hub_challenge, [FromQuery] string hub_verify_token)
        {

            return Ok(hub_challenge);
        }

    }
}
