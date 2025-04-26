using System.Threading.Tasks;

namespace LigChat.Backend.Application.Interface.S3StorageInterface
{
    public interface IS3StorageService
    {
        Task<string> UploadFileAsync(string base64Content, string fileName, string contentType, string? mimeType = null);
        Task DeleteFileAsync(string fileUrl);
    }
} 