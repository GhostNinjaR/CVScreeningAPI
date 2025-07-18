using System.ComponentModel.DataAnnotations;

namespace CVScreeningAPI.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Range(0, 50)]
        public int Experience { get; set; }

        [StringLength(255)]
        public string CvFileName { get; set; } = string.Empty;

        [StringLength(500)]
        public string CvFilePath { get; set; } = string.Empty;

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        [StringLength(20)]
        public string Status { get; set; } = "pending";

        [Range(1, 5)]
        public int? Rating { get; set; }

        public DateTime? InterviewDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();
        public virtual ICollection<CandidateSkill> CandidateSkills { get; set; } = new List<CandidateSkill>();
    }
}
