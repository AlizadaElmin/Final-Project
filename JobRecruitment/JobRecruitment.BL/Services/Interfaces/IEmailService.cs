namespace JobRecruitment.BL.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string? email, string forgotToken, string reason);
}