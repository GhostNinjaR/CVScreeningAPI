using CVScreeningAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CVScreeningAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CandidateSkill> CandidateSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Comments)
                .WithOne(c => c.Candidate)
                .HasForeignKey(c => c.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Interviews)
                .WithOne(i => i.Candidate)
                .HasForeignKey(i => i.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CandidateSkill>()
                .HasKey(cs => new { cs.CandidateId, cs.SkillId });

            modelBuilder.Entity<CandidateSkill>()
                .HasOne(cs => cs.Candidate)
                .WithMany(c => c.CandidateSkills)
                .HasForeignKey(cs => cs.CandidateId);

            modelBuilder.Entity<CandidateSkill>()
                .HasOne(cs => cs.Skill)
                .WithMany(s => s.CandidateSkills)
                .HasForeignKey(cs => cs.SkillId);
        }
    }
}
