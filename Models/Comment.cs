using System.ComponentModel.DataAnnotations;

namespace CVScreeningAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public int CandidateId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Author { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Navigation property
        public virtual Candidate Candidate { get; set; } = null!;
    }
}
