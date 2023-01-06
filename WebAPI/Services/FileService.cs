using Domain.Models;
using Infrastructure.Services.Interfaces;
using System.Net;

namespace WebAPI.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILoggerManager _logger;
        public FileService(IWebHostEnvironment env, ILoggerManager logger)
        {
            _env = env;
            _logger = logger;
        }
        public async Task<IList<UploadResult>> UploadFile(IEnumerable<IFormFile> files, string filePath, string domain)
        {
            var maxAllowedFiles = 3;
            long maxFileSize = 1024 * 1024 * 15;
            var filesProcessed = 0;
            List<UploadResult> uploadResults = new();


            foreach (var file in files)
            {
                var uploadResult = new UploadResult();
                var untrustedFileName = file.FileName;
                uploadResult.FileName = untrustedFileName;
                var trustedFileNameForDisplay =
                    WebUtility.HtmlEncode(untrustedFileName);

                if (filesProcessed < maxAllowedFiles)
                {
                    if (file.Length == 0)
                    {
                        _logger.LogInfo($"{trustedFileNameForDisplay} length is 0 (Err: 1)");
                        uploadResult.ErrorCode = 1;
                    }
                    else if (file.Length > maxFileSize)
                    {
                        _logger.LogInfo($"{trustedFileNameForDisplay} of {file.Length} bytes is " +
                            "larger than the limit of {maxFileSize} bytes (Err: 2)");
                        uploadResult.ErrorCode = 2;
                    }
                    else
                    {
                        try
                        {
                            var path = Path.Combine(_env.ContentRootPath, filePath, file.FileName);

                            await using FileStream fs = new(path, FileMode.Create);
                            await file.CopyToAsync(fs);

                            _logger.LogInfo($"{trustedFileNameForDisplay} saved at {path}");
                            uploadResult.Uploaded = true;
                            uploadResult.StoredFileName = Path.Combine(domain, filePath, file.FileName);
                        }
                        catch (IOException ex)
                        {
                            _logger.LogError($"{trustedFileNameForDisplay} error on upload (Err: 3): {ex.Message}");
                            uploadResult.ErrorCode = 3;
                        }
                    }

                    filesProcessed++;
                }
                else
                {
                    _logger.LogInfo($"{trustedFileNameForDisplay} not uploaded because the " +
                        "request exceeded the allowed {maxAllowedFiles} of files (Err: 4)");
                    uploadResult.ErrorCode = 4;
                }

                uploadResults.Add(uploadResult);
            }
            return uploadResults;
        }

        public UploadResult GetFile(string filePath, string fileName, string domain)
        {
            var path = Path.Combine(_env.ContentRootPath, filePath, fileName);
            UploadResult file = new();
            file.FileName = fileName;
            file.StoredFileName = Path.Combine(domain, filePath, file.FileName);
            if (!File.Exists(path))
                file.ErrorCode = 1; // file not exist

            return file;
        }

        public bool DeleteFile(string filePath, string fileName)
        {
            try
            {
                // Check if file exists with its full path    
                if (File.Exists(Path.Combine(filePath, fileName)))
                {
                    // If file found, delete it    
                    File.Delete(Path.Combine(filePath, fileName));
                    _logger.LogInfo($"Delete file :[{fileName}] successfully ");
                    return true;
                }
                else
                {
                    _logger.LogWarn($"Can't not find the file [{fileName}]");
                    return false;
                }

            }
            catch (IOException ioExp)
            {
                _logger.LogError($"[{fileName}] error on delete : {ioExp.Message}");
                return false;
            }
        }
    }
}
