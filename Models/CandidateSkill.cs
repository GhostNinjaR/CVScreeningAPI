namespace CVScreeningAPI.Models
{
    public class CandidateSkill
    {
        public int CandidateId { get; set; }
        public int SkillId { get; set; }

        public virtual Candidate Candidate { get; set; } = null!;
        public virtual Skill Skill { get; set; } = null!;
    }
}
