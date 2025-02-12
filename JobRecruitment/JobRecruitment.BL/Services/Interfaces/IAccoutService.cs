using JobRecruitment.BL.DTOs.UserDtos;

namespace JobRecruitment.BL.Services.Interfaces;

public interface IAccoutService
{ 
    Task<string> RegisterAsync(RegisterDto dto);
    Task<bool> LoginAsync(LoginDto dto);
 
}