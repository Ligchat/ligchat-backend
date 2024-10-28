using LigChat.Backend.Application.Common.Mappings.UserActionResults;
using LigChat.Backend.Application.Interface.UserInterface;
using LigChat.Backend.Domain.DTOs.UserDto;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LigChat.Backend.Domain.Entities;

namespace LigChat.Com.Api.Mvc.UserMvc.Service
{
    public class UserService : IUserServiceInterface
    {
        private readonly IUserRepositoryInterface _userRepository;
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;
        private readonly string _bucketName;

        public UserService(
            IUserRepositoryInterface userRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;

            // Lê o nome do bucket e as credenciais diretamente do appsettings.json
            var awsAccessKey = _configuration["AWS:AccessKey"];
            var awsSecretKey = _configuration["AWS:SecretKey"];
            var region = _configuration["AWS:Region"];
            _bucketName = _configuration["AWS:BucketName"] ?? throw new ArgumentNullException("Bucket name not found in configuration");

            // Configura o cliente S3 com as credenciais
            var s3Config = new AmazonS3Config { RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region) };
            _s3Client = new AmazonS3Client(awsAccessKey, awsSecretKey, s3Config);
        }

        public SingleUserResponse? Delete(int id)
        {
            var deletedUser = _userRepository.Delete(id);
            if (deletedUser == null)
            {
                return new SingleUserResponse("User not found", "404", null);
            }

            var responseDto = new UserViewModel(
                deletedUser.Id,
                deletedUser.Name,
                deletedUser.Email,
                deletedUser.PhoneWhatsapp,
                deletedUser.AvatarUrl,
                deletedUser.IsAdmin,
                deletedUser.Status
            );

            return new SingleUserResponse("User deleted successfully", "200", responseDto);
        }

        public UserListResponse GetAll()
        {
            var users = _userRepository.GetAll();
            var userDtos = users.Select(user => new UserViewModel(
                user.Id,
                user.Name,
                user.Email,
                user.PhoneWhatsapp,
                user.AvatarUrl,
                user.IsAdmin,
                user.Status
            )).ToList(); // Corrigido para garantir que a lista é retornada corretamente

            return new UserListResponse("Success", "200", userDtos);
        }

        public SingleUserResponse? GetByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null)
            {
                return new SingleUserResponse("User not found", "404", null);
            }

            var userDto = new UserViewModel(
                user.Id,
                user.Name,
                user.Email,
                user.PhoneWhatsapp,
                user.AvatarUrl,
                user.IsAdmin,
                user.Status
            );

            return new SingleUserResponse("Success", "200", userDto);
        }

        public SingleUserResponse? GetById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return new SingleUserResponse("User not found", "404", null);
            }

            var userDto = new UserViewModel(
                user.Id,
                user.Name,
                user.Email,
                user.PhoneWhatsapp,
                user.AvatarUrl,
                user.IsAdmin,
                user.Status

            );

            return new SingleUserResponse("Success", "200", userDto);
        }

        public SingleUserResponse? Save(CreateUserRequestDTO userDto)
        {
            // Valida os dados do usuário
            if (string.IsNullOrWhiteSpace(userDto.Name) ||
                string.IsNullOrWhiteSpace(userDto.Email))
            {
                return new SingleUserResponse("Invalid request", "400", null);
            }


            // Cria um novo usuário a partir do DTO
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PhoneWhatsapp = userDto.PhoneWhatsapp,
                AvatarUrl = userDto.AvatarUrl,
                IsAdmin = userDto.IsAdmin,
                Status = userDto.Status
            };

            // Adiciona o usuário ao banco de dados para obter o ID
            var savedUser = _userRepository.Add(user);

            var responseDto = new UserViewModel(
                savedUser.Id,
                savedUser.Name,
                savedUser.Email,
                savedUser.PhoneWhatsapp,
                savedUser.AvatarUrl,
                savedUser.IsAdmin,
                savedUser.Status
            );

            return new SingleUserResponse("User created successfully", "201", responseDto);
        }

        public SingleUserResponse Update(int id, UpdateUserRequestDTO userDto)
        {
            // Valida os dados do usuário
            if (string.IsNullOrWhiteSpace(userDto.Name) ||
                string.IsNullOrWhiteSpace(userDto.Email))
            {
                return new SingleUserResponse("Invalid request", "400", null);
            }

            var existingUser = _userRepository.GetById(id);
            if (existingUser == null)
            {
                return new SingleUserResponse("User not found", "404", null);
            }

            // Atualiza os dados do usuário
            existingUser.Name = userDto.Name;
            existingUser.Email = userDto.Email;
            existingUser.PhoneWhatsapp = userDto.PhoneWhatsapp;
            existingUser.AvatarUrl = userDto.AvatarUrl;
            existingUser.IsAdmin = userDto.IsAdmin;
            existingUser.Status = userDto.Status;

            // Atualiza o usuário no banco de dados
            var savedUser = _userRepository.Update(existingUser);

            var responseDto = new UserViewModel(
                savedUser.Id,
                savedUser.Name,
                savedUser.Email,
                savedUser.PhoneWhatsapp,
                savedUser.AvatarUrl,
                savedUser.IsAdmin,
                savedUser.Status
            );

            return new SingleUserResponse("User updated successfully", "200", responseDto);
        }

        // Métodos auxiliares...

        private bool IsBase64String(string base64String)
        {
            if (string.IsNullOrEmpty(base64String)) return false;
            var base64Regex = new Regex(@"^data:image\/(png|jpg|jpeg);base64,");
            return base64Regex.IsMatch(base64String);
        }

        public async Task SaveVerificationCode(int userId, string verificationCode)
        {
            var user = _userRepository.GetById(userId);
            if (user == null) throw new Exception("User not found.");

            // Salva o código de verificação com expiração
            user.VerificationCode = verificationCode;
            user.VerificationCodeExpiresAt = DateTime.UtcNow.AddMinutes(5); // Expira em 5 minutos

            _userRepository.Update(user);
        }

        public async Task<bool> ValidateVerificationCode(string email, string code)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null || user.VerificationCodeExpiresAt < DateTime.UtcNow)
            {
                return false; // Usuário não encontrado ou código expirado
            }

            return user.VerificationCode == code; // Retorna true se o código é válido
        }

        private string SaveImageToS3(int userId, string base64Image)
        {
            try
            {
                // Remove o prefixo 'data:image/png;base64,' da string
                var base64Data = base64Image.Substring(base64Image.IndexOf(",") + 1);
                var imageData = Convert.FromBase64String(base64Data);

                // Cria um nome de arquivo único baseado no ID do usuário
                var fileName = $"avatars/{userId}.png";

                using (var stream = new MemoryStream(imageData))
                {
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = stream,
                        Key = fileName,
                        BucketName = _bucketName
                    };

                    var transferUtility = new TransferUtility(_s3Client);
                    transferUtility.Upload(uploadRequest);
                }

                // Retorna a URL pública da imagem no S3
                return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload image to S3", ex);
            }
        }
    }
}
