using System.ComponentModel.DataAnnotations;

namespace CVScreeningAPI.Models
{
    public class Interview
    {
        public int Id { get; set; }

        [Required]
        public int CandidateId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "scheduled";

        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public virtual Candidate Candidate { get; set; } = null!;
    }
}
