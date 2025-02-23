using JobRecruitment.BL.ExternalServices.Interfaces;
using JobRecruitment.BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobRecruitment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailSendController(IEmailService _emailService,ICurrentUser _user):ControllerBase
{
    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> SendMail()
    {
        string? email = _user.GetEmail();

        if (string.IsNullOrEmpty(email))
            return Unauthorized("User email not found");

        await _emailService.SendEmailAsync("confirmation",email,null);
        return Content("Email sent");
    }
}