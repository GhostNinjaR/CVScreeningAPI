using AutoMapper;
using CVScreeningAPI.Data;
using CVScreeningAPI.DTOs;
using CVScreeningAPI.Models;
using CVScreeningAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVScreeningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CandidatesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly ISecurityService _securityService;

        public CandidatesController(
            ApplicationDbContext context,
            IMapper mapper,
            IFileService fileService,
            ISecurityService securityService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
            _securityService = securityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDto>>> GetCandidates(
            [FromQuery] string? sortBy = null,
            [FromQuery] string? filter = null)
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
            return Ok(_mapper.Map<IEnumerable<CandidateDto>>(candidates));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDto>> GetCandidate(int id)
        {
            var candidate = await _context.Candidates
                .Include(c => c.Comments)
                .Include(c => c.Interviews)
                .Include(c => c.CandidateSkills)
                .ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (candidate == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CandidateDto>(candidate));
        }

        [HttpPost]
        public async Task<ActionResult<CandidateDto>> CreateCandidate([FromBody] CreateCandidateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var candidate = _mapper.Map<Candidate>(createDto);
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCandidate), new { id = candidate.Id }, _mapper.Map<CandidateDto>(candidate));
        }

        [HttpPost("upload-cv")]
        public async Task<ActionResult<CandidateDto>> UploadCV([FromForm] UploadCVDto uploadDto)
        {
            if (uploadDto.File == null || uploadDto.File.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            // Validate file
            var validationResult = await _securityService.ValidateFileAsync(uploadDto.File);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ErrorMessage);
            }

            // Save file
            var fileName = await _fileService.SaveFileAsync(uploadDto.File);
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("Failed to save file");
            }

            // Create candidate
            var candidate = new Candidate
            {
                Name = uploadDto.Name,
                Email = uploadDto.Email,
                Phone = uploadDto.Phone,
                Experience = uploadDto.Experience,
                CvFileName = uploadDto.File.FileName,
                CvFilePath = fileName
            };

            // Process CV for skills and keywords
            var cvAnalysis = await _securityService.AnalyzeCVAsync(uploadDto.File);
            candidate.Skills = string.Join(",", cvAnalysis.Skills);

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCandidate), new { id = candidate.Id }, _mapper.Map<CandidateDto>(candidate));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCandidate(int id, [FromBody] UpdateCandidateDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return BadRequest();
            }

            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            _mapper.Map(updateDto, candidate);
            candidate.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto statusDto)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            candidate.Status = statusDto.Status;
            candidate.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}/rating")]
        public async Task<IActionResult> UpdateRating(int id, [FromBody] UpdateRatingDto ratingDto)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            candidate.Rating = ratingDto.Rating;
            candidate.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CandidateExists(int id)
        {
            return _context.Candidates.Any(e => e.Id == id);
        }
    }
}
