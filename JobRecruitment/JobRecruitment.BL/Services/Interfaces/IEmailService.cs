namespace JobRecruitment.BL.Services.Interfaces;

public interface IEmailService
{
    Task SendEmail();
    // Task AccountVerify(string userToken);
}