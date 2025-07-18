using CVScreeningAPI.DTOs;

namespace CVScreeningAPI.Services
{
    public interface ISecurityService
    {
        Task<FileValidationResult> ValidateFileAsync(IFormFile file);
        Task<CVAnalysisResult> AnalyzeCVAsync(IFormFile file);
        string SanitizeInput(string input);
        bool ValidateEmail(string email);
    }

    public class FileValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }

    public class CVAnalysisResult
    {
        public List<string> Skills { get; set; } = new();
        public int Experience { get; set; }
        public List<string> Keywords { get; set; } = new();
    }
}
