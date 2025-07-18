using CVScreeningAPI.DTOs;
using CVScreeningAPI.Models;

namespace CVScreeningAPI.Services
{
    public interface ICandidateService
    {
        Task<IEnumerable<CandidateDto>> GetCandidatesAsync(string? sortBy = null, string? filter = null);
        Task<CandidateDto?> GetCandidateByIdAsync(int id);
        Task<CandidateDto> CreateCandidateAsync(CreateCandidateDto dto);
        Task<bool> UpdateCandidateAsync(int id, UpdateCandidateDto dto);
        Task<bool> DeleteCandidateAsync(int id);
        Task<bool> UpdateStatusAsync(int id, string status);
        Task<bool> UpdateRatingAsync(int id, int rating);
    }
}
