﻿using LigChat.Backend.Application.Common.Mappings.UserActionResults;
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
using LigChat.Data.Repositories;
using tests_.src.Domain.Entities.LigChat.Backend.Domain.Entities;
using LigChat.Data.Interfaces.IRepositories;

namespace LigChat.Com.Api.Mvc.UserMvc.Service
{
    public class UserService : IUserServiceInterface
    {
        private readonly IUserRepositoryInterface _userRepository;
        private readonly IAmazonS3 _s3Client;
        private readonly IUserSectorRepositoryInterface _userSector;
        private readonly IConfiguration _configuration;
        private readonly string _bucketName;
        private readonly ISectorRepositoryInterface _sectorRepository;

        public UserService(
            IUserRepositoryInterface userRepository,
            IConfiguration configuration,
            IUserSectorRepositoryInterface userSector,
            ISectorRepositoryInterface sectorRepository
            )
            
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _userSector = userSector;
            _sectorRepository = sectorRepository;

            var awsAccessKey = _configuration["AWS:AccessKey"];
            var awsSecretKey = _configuration["AWS:SecretKey"];
            var region = _configuration["AWS:Region"];
            _bucketName = _configuration["AWS:BucketName"] ?? throw new ArgumentNullException("Bucket name not found in configuration");

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

        public UserListResponse GetAll(int? invitedBy = null)
        {
            IEnumerable<User> users;

            if (invitedBy.HasValue)
            {
                users = _userRepository.GetAll().Where(user => user.InvitedBy == invitedBy.Value);

                var invitedUser = _userRepository.GetAll().FirstOrDefault(user => user.Id == invitedBy.Value);
                if (invitedUser != null)
                {
                    users = users.Append(invitedUser);
                }
            }
            else
            {
                users = _userRepository.GetAll();
            }

            var userDtos = users.Select(user =>
            {
                var userSectors = _userSector.GetAllByUserId(user.Id);
                var sectorIds = userSectors.Select(us => us.SectorId).ToList();

                var userViewModel = new UserViewModel(
                    user.Id,
                    user.Name,
                    user.Email,
                    user.PhoneWhatsapp,
                    user.AvatarUrl,
                    user.IsAdmin,
                    user.Status,
                    user.InvitedBy
                );

                // Adiciona informações dos setores
                foreach (var userSector in userSectors)
                {
                    var sector = _sectorRepository.GetById(userSector.SectorId);
                    if (sector != null)
                    {
                        userViewModel.Sectors.Add(new SectorInfo(sector.Id, sector.Name));
                    }
                }

                return userViewModel;
            }).ToList();

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

        public SingleUserResponse Save(CreateUserRequestDTO userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Name) || string.IsNullOrWhiteSpace(userDto.Email))
            {
                return new SingleUserResponse("Invalid request", "400", null);
            }

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PhoneWhatsapp = userDto.PhoneWhatsapp,
                AvatarUrl = userDto.AvatarUrl,
                IsAdmin = userDto.IsAdmin,
                Status = userDto.Status,
                InvitedBy = userDto.InvitedBy // Atribui o ID do convidador
            };

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
            if (string.IsNullOrWhiteSpace(userDto.Name) || string.IsNullOrWhiteSpace(userDto.Email))
            {
                return new SingleUserResponse("Invalid request", "400", null);
            }

            var existingUser = _userRepository.GetById(id);
            if (existingUser == null)
            {
                return new SingleUserResponse("User not found", "404", null);
            }

            existingUser.Name = userDto.Name;
            existingUser.Email = userDto.Email;
            existingUser.PhoneWhatsapp = userDto.PhoneWhatsapp;
            existingUser.IsAdmin = userDto.IsAdmin;
            existingUser.Status = userDto.Status;

            // Verifica se uma imagem foi enviada
            if (!string.IsNullOrWhiteSpace(userDto.AvatarUrl) && IsBase64String(userDto.AvatarUrl))
            {
                existingUser.AvatarUrl = SaveImageToS3(existingUser.Id, userDto.AvatarUrl);
            }
            else
            {
                existingUser.AvatarUrl = userDto.AvatarUrl; // Mantém a URL existente se não houver nova imagem
            }

            var savedUser = _userRepository.Update(existingUser);

            // Atualiza os setores do usuário
            if (userDto.Sectors != null)
            {
                // Remove todos os setores atuais do usuário
                _userSector.DeleteAllByUserId(savedUser.Id);

                // Adiciona os novos setores
                foreach (var sector in userDto.Sectors)
                {
                    var userSector = new UserSector
                    {
                        UserId = savedUser.Id,
                        SectorId = sector.Id,
                        IsShared = true
                    };
                    _userSector.Save(userSector);
                }
            }

            // Busca os setores atualizados para retornar na resposta
            var userSectors = _userSector.GetAllByUserId(savedUser.Id);
            var sectorInfoList = new List<SectorInfo>();

            foreach (var userSector in userSectors)
            {
                var sector = _sectorRepository.GetById(userSector.SectorId);
                if (sector != null)
                {
                    sectorInfoList.Add(new SectorInfo(sector.Id, sector.Name));
                }
            }

            var responseDto = new UserViewModel(
                savedUser.Id,
                savedUser.Name,
                savedUser.Email,
                savedUser.PhoneWhatsapp,
                savedUser.AvatarUrl,
                savedUser.IsAdmin,
                savedUser.Status,
                savedUser.InvitedBy
            );
            responseDto.Sectors = sectorInfoList;

            return new SingleUserResponse("User updated successfully", "200", responseDto);
        }

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

            user.VerificationCode = verificationCode;
            user.VerificationCodeExpiresAt = DateTime.UtcNow.AddMinutes(5);

            _userRepository.Update(user);
        }

        public async Task<bool> ValidateVerificationCode(string email, string code)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null || user.VerificationCodeExpiresAt < DateTime.UtcNow)
            {
                return false;
            }

            return user.VerificationCode == code;
        }

        private string SaveImageToS3(int userId, string base64Image)
        {
            try
            {
                var base64Data = base64Image.Substring(base64Image.IndexOf(",") + 1);
                var imageData = Convert.FromBase64String(base64Data);

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

                return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload image to S3", ex);
            }
        }
    }
}
