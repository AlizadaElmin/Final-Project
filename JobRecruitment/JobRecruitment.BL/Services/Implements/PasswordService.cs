using JobRecruitment.BL.DTOs.PasswordDtos;
using JobRecruitment.BL.DTOs.UserDtos;
using JobRecruitment.BL.Exceptions.PasswordException;
using JobRecruitment.BL.Exceptions.UserException;
using JobRecruitment.BL.Services.Interfaces;
using JobRecruitment.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ChangePasswordFailedException = JobRecruitment.BL.Exceptions.PasswordException.ChangePasswordFailedException;

namespace JobRecruitment.BL.Services.Implements;

public class PasswordService(UserManager<User> _userManager,IEmailService _emailService):IPasswordService
{
        public async Task ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new UserNotFoundException();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _emailService.SendEmailAsync("forgotPassword",email,token);
        }

        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) throw new UserNotFoundException();

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            if (!result.Succeeded)
            {
                throw new PasswordResetFailedException();
            }
        }

        public async Task ChangePasswordAsync(ChangePasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) throw new UserNotFoundException();

            var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
            if (!result.Succeeded)
            {
                throw new ChangePasswordFailedException();
            }
        }
}

