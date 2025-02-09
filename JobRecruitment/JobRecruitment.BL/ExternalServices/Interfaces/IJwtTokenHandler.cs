using JobRecruitment.Core.Entities;

namespace JobRecruitment.BL.ExternalServices.Interfaces;

public interface IJwtTokenHandler
{
    Task<string> CreateToken(User user,int hours);
}