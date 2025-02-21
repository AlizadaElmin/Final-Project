using JobRecruitment.BL.DTOs.UserDtos;

namespace JobRecruitment.BL.Services.Interfaces;

public interface IAccoutService
{ 
    Task<string> RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto);
 
}