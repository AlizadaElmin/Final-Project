namespace JobRecruitment.BL.Services.Interfaces;

public interface IAccountVerifyService
{
    Task AccountVerify(string userToken);
}