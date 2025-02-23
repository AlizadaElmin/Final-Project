using JobRecruitment.BL.DTOs.UserDtos;

namespace JobRecruitment.BL.ExternalServices.Interfaces;

public interface ICurrentUser
{
    string GetId();
    string GetUserName();
    string GetEmail();
    string GetName();
    int GetRole();
    Task<UserGetDto> GetUserAsync();
}