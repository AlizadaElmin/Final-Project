using JobRecruitment.BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobRecruitment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailSendController(IEmailService _emailService):ControllerBase
{
    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> SendMail()
    {
        await _emailService.SendEmail();
        return Content("Email sent");
    }

}