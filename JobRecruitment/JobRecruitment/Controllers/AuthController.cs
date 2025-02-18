using JobRecruitment.BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobRecruitment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthControlle(IEmailService _service) : ControllerBase
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> SendMail()
        {
            await _service.SendEmail();
            return Content("Email sended");
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Verify(string token)
        {
            await _service.AccountVerify(token);
            return Content("Email confirmed");
        }
    }
}