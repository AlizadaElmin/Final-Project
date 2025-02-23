using System.IdentityModel.Tokens.Jwt;
using JobRecruitment.BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobRecruitment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IEmailService _emailService,IAccountVerifyService _verifyService) : ControllerBase
    {
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Verify(string token)
        {
            await _verifyService.AccountVerify(token);
            return Content("Email confirmed");
        }
    }
}