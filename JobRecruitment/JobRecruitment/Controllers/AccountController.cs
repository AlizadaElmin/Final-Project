using JobRecruitment.BL.DTOs.UserDtos;
using JobRecruitment.BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobRecruitment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccoutService _service):ControllerBase
{
    [HttpPost("[action]")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        await _service.RegisterAsync(registerDto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        return Ok(await _service.LoginAsync(loginDto));
    }
}