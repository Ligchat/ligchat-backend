using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using LigChat.Backend.Application.Interface.S3StorageInterface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LigChat.Backend.Application.Services.Storage
{
    public class S3StorageService : IS3StorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        private readonly ILogger<S3StorageService> _logger;

        public S3StorageService(IConfiguration configuration, ILogger<S3StorageService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
            ValidateAwsConfiguration(configuration);
            
            var accessKey = configuration["AWS:AccessKey"];
            var secretKey = configuration["AWS:SecretKey"];
            var region = configuration["AWS:Region"];
            _bucketName = configuration["AWS:BucketName"];

            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var config = new AmazonS3Config
            {
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region)
            };

            _s3Client = new AmazonS3Client(credentials, config);
        }

        private void ValidateAwsConfiguration(IConfiguration configuration)
        {
            var accessKey = configuration["AWS:AccessKey"];
            var secretKey = configuration["AWS:SecretKey"];
            var region = configuration["AWS:Region"];
            var bucketName = configuration["AWS:BucketName"];

            _logger.LogInformation("Validating AWS configuration...");
            _logger.LogInformation($"Access Key exists: {!string.IsNullOrEmpty(accessKey)}");
            _logger.LogInformation($"Secret Key exists: {!string.IsNullOrEmpty(secretKey)}");
            _logger.LogInformation($"Region: {region}");
            _logger.LogInformation($"Bucket Name: {bucketName}");

            if (string.IsNullOrEmpty(accessKey))
                throw new ArgumentException("AWS:AccessKey configuration is missing");
            
            if (string.IsNullOrEmpty(secretKey))
                throw new ArgumentException("AWS:SecretKey configuration is missing");
            
            if (string.IsNullOrEmpty(region))
                throw new ArgumentException("AWS:Region configuration is missing");

            if (string.IsNullOrEmpty(bucketName))
                throw new ArgumentException("AWS:BucketName configuration is missing");

            _logger.LogInformation("AWS configuration validation completed successfully");
        }

        public async Task<string> UploadFileAsync(string base64Content, string fileName, string contentType, string? mimeType = null)
        {
            try
            {
                _logger.LogInformation($"Starting file upload: {fileName}");

                if (string.IsNullOrEmpty(base64Content))
                    throw new ArgumentException("Base64 content cannot be null or empty", nameof(base64Content));

                if (string.IsNullOrEmpty(fileName))
                    throw new ArgumentException("File name cannot be null or empty", nameof(fileName));

                var fileBytes = Convert.FromBase64String(base64Content);
                var key = $"uploads/{DateTime.UtcNow:yyyy/MM/dd}/{Guid.NewGuid()}-{fileName}";

                _logger.LogInformation($"Uploading file to S3. Bucket: {_bucketName}, Key: {key}");

                // Usar o MimeType fornecido se disponível
                string finalMimeType;
                if (!string.IsNullOrEmpty(mimeType))
                {
                    finalMimeType = mimeType;
                }
                else
                {
                    var extension = Path.GetExtension(fileName).ToLower();
                    
                    finalMimeType = contentType.ToLower() switch
                    {
                        "audio" => extension switch
                        {
                            ".mp3" => "audio/mpeg",
                            ".wav" => "audio/wav",
                            ".ogg" => "audio/ogg",
                            _ => "audio/mpeg" // default para áudio
                        },
                        "file" => extension switch
                        {
                            ".pdf" => "application/pdf",
                            ".doc" => "application/msword",
                            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                            ".xls" => "application/vnd.ms-excel",
                            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            ".txt" => "text/plain",
                            ".csv" => "text/csv",
                            ".zip" => "application/zip",
                            ".rar" => "application/x-rar-compressed",
                            _ => "application/octet-stream" // tipo genérico para outros arquivos
                        },
                        "text" => "text/plain",
                        _ => "application/octet-stream" // tipo genérico para casos não tratados
                    };
                }

                _logger.LogInformation($"Using MIME type: {finalMimeType} for file: {fileName}");

                using var memoryStream = new MemoryStream(fileBytes);
                var putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key,
                    InputStream = memoryStream,
                    ContentType = finalMimeType
                };

                await _s3Client.PutObjectAsync(putRequest);
                var fileUrl = $"https://{_bucketName}.s3.amazonaws.com/{key}";
                
                _logger.LogInformation($"File uploaded successfully. URL: {fileUrl}");
                return fileUrl;
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, "Invalid base64 content provided");
                throw new ArgumentException("Invalid base64 content provided", nameof(base64Content), ex);
            }
            catch (AmazonS3Exception ex)
            {
                _logger.LogError(ex, $"AWS S3 error. Error Code: {ex.ErrorCode}");
                throw new Exception($"AWS S3 error: {ex.Message}. Error Code: {ex.ErrorCode}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file to S3");
                throw new Exception($"Error uploading file to S3: {ex.Message}", ex);
            }
        }

        public async Task DeleteFileAsync(string fileUrl)
        {
            try
            {
                _logger.LogInformation($"Starting file deletion: {fileUrl}");

                if (string.IsNullOrEmpty(fileUrl))
                    throw new ArgumentException("File URL cannot be null or empty", nameof(fileUrl));

                var uri = new Uri(fileUrl);
                var key = uri.AbsolutePath.TrimStart('/');

                _logger.LogInformation($"Deleting file from S3. Bucket: {_bucketName}, Key: {key}");

                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key
                };

                await _s3Client.DeleteObjectAsync(deleteRequest);
                _logger.LogInformation("File deleted successfully");
            }
            catch (UriFormatException ex)
            {
                _logger.LogError(ex, "Invalid file URL format");
                throw new ArgumentException("Invalid file URL format", nameof(fileUrl), ex);
            }
            catch (AmazonS3Exception ex)
            {
                _logger.LogError(ex, $"AWS S3 error. Error Code: {ex.ErrorCode}");
                throw new Exception($"AWS S3 error: {ex.Message}. Error Code: {ex.ErrorCode}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file from S3");
                throw new Exception($"Error deleting file from S3: {ex.Message}", ex);
            }
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".mp3" => "audio/mpeg",
                ".mp4" => "video/mp4",
                _ => "application/octet-stream"
            };
        }
    }
} 