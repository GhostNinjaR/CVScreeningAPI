namespace CVScreeningAPI.DTOs
{
    public class CandidateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int Experience { get; set; }
        public string Skills { get; set; } = string.Empty;
        public string CvFileName { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }
        public string Status { get; set; } = "pending";
        public int? Rating { get; set; }
        public DateTime? InterviewDate { get; set; }
        public List<CommentDto> Comments { get; set; } = new();
        public List<InterviewDto> Interviews { get; set; } = new();
    }

    public class CreateCandidateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int Experience { get; set; }
        public string Skills { get; set; } = string.Empty;
    }

    public class UpdateCandidateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int Experience { get; set; }
        public string Skills { get; set; } = string.Empty;
    }

    public class UpdateStatusDto
    {
        public string Status { get; set; } = string.Empty;
    }

    public class UpdateRatingDto
    {
        public int Rating { get; set; }
    }

    public class UploadCVDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int Experience { get; set; }
        public IFormFile File { get; set; } = null!;
    }

    public class CommentDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }

    public class InterviewDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
