using JobRecruitment.BL.DTOs.UserDtos;
using JobRecruitment.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobRecruitment.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PasswordController(IPasswordService _passwordService):ControllerBase
{
    [HttpPost("[action]")]
    public async Task<ActionResult> ForgotPassword(string email)
    {
        await _passwordService.ForgotPasswordAsync(email);
        return Ok();
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult> ResetPassword(ResetPasswordDto dto)
    {
        await _passwordService.ResetPasswordAsync(dto);
        return Ok();
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult> ChangePassword(ChangePasswordDto dto)
    {
        await _passwordService.ChangePasswordAsync(dto);
        return Ok();
    }
}