using AutoMapper;
using CVScreeningAPI.Data;
using CVScreeningAPI.DTOs;
using CVScreeningAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CVScreeningAPI.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CandidateService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CandidateDto>> GetCandidatesAsync(string? sortBy = null, string? filter = null)
        {
            var query = _context.Candidates
                .Include(c => c.Comments)
                .Include(c => c.Interviews)
                .Include(c => c.CandidateSkills)
                .ThenInclude(cs => cs.Skill)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(c => 
                    c.Name.Contains(filter) || 
                    c.Email.Contains(filter) ||
                    c.Skills.Contains(filter));
            }

            query = sortBy?.ToLower() switch
            {
                "name" => query.OrderBy(c => c.Name),
                "experience" => query.OrderByDescending(c => c.Experience),
                "date" => query.OrderByDescending(c => c.UploadDate),
                "status" => query.OrderBy(c => c.Status),
                _ => query.OrderByDescending(c => c.UploadDate)
            };

            var candidates = await query.ToListAsync();
            return _mapper.Map<IEnumerable<CandidateDto>>(candidates);
        }

        public async Task<CandidateDto?> GetCandidateByIdAsync(int id)
        {
            var candidate = await _context.Candidates
                .Include(c => c.Comments)
                .Include(c => c.Interviews)
                .Include(c => c.CandidateSkills)
                .ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.Id == id);

            return candidate == null ? null : _mapper.Map<CandidateDto>(candidate);
        }

        public async Task<CandidateDto> CreateCandidateAsync(CreateCandidateDto dto)
        {
            var candidate = _mapper.Map<Candidate>(dto);
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
            return _mapper.Map<CandidateDto>(candidate);
        }

        public async Task<bool> UpdateCandidateAsync(int id, UpdateCandidateDto dto)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null) return false;

            _mapper.Map(dto, candidate);
            candidate.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCandidateAsync(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null) return false;

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null) return false;

            candidate.Status = status;
            candidate.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRatingAsync(int id, int rating)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null) return false;

            candidate.Rating = rating;
            candidate.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
