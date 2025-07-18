using CVScreeningAPI.DTOs;

namespace CVScreeningAPI.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailDto emailDto);
        Task<bool> SendInterviewInvitationAsync(int candidateId, DateTime interviewDate, TimeSpan interviewTime);
        Task<bool> SendApplicationReceivedAsync(int candidateId);
        Task<bool> SendStatusUpdateAsync(int candidateId, string status);
    }

    public class EmailDto
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public int? CandidateId { get; set; }
    }
}
