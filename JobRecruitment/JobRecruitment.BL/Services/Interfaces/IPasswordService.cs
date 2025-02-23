using JobRecruitment.BL.DTOs.UserDtos;

namespace JobRecruitment.BL.Services.Interfaces;

public interface IPasswordService
{
        Task ForgotPasswordAsync(string email);
        Task ResetPasswordAsync(ResetPasswordDto dto);
        Task ChangePasswordAsync(ChangePasswordDto dto);
}