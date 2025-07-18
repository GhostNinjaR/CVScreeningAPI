namespace CVScreeningAPI.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file);
        Task<bool> DeleteFileAsync(string fileName);
        Task<byte[]?> GetFileAsync(string fileName);
        Task<bool> FileExistsAsync(string fileName);
    }
}
