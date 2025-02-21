using System.IdentityModel.Tokens.Jwt;
using JobRecruitment.BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobRecruitment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IEmailService _service) : ControllerBase
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> SendMail()
        {
            await _service.SendEmail();
            return Content("Email sent");
        }
        
        // [HttpPost("[action]")]
        // public async Task<IActionResult> Verify(string token)
        // {
        //     await _service.AccountVerify(token);
        //     return Content("Email confirmed");
        // }
    }
}