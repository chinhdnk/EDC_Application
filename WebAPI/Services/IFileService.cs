using Domain.Models;

namespace WebAPI.Services
{
    public interface IFileService
    {
        bool DeleteFile(string filePath, string fileName);
        UploadResult GetFile(string filePath, string fileName, string domain);
        Task<IList<UploadResult>> UploadFile(IEnumerable<IFormFile> files, string filePath, string domain);
    }
}