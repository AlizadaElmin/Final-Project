using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using JobRecruitment.BL.Constants;
using JobRecruitment.BL.DTOs.Options;
using JobRecruitment.BL.Exceptions.Common;
using JobRecruitment.BL.Services.Interfaces;
using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Enums;
using JobRecruitment.DAL.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace JobRecruitment.BL.Services.Implements;

public class EmailService:IEmailService
{
    private readonly UserManager<User> _userManager;
    private readonly JobRecruitmentDbContext _context;
    private readonly MailAddress _from;
    private readonly EmailOptions _opt;
    private readonly IMemoryCache _cache;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmailService(IOptions<EmailOptions> options,JobRecruitmentDbContext context,UserManager<User> userManager,IMemoryCache cache,IHttpContextAccessor httpContextAccessor)
    {
        _opt = options.Value;
        _from = new MailAddress(_opt.Sender,"Job Recruitment");
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _cache = cache;
        _context = context;
    }
    public async Task SendEmailAsync(string reason,string? email,string? forgotToken)
    {
        string token = null;
        if (reason == "confirmation")
        { 
            token = Guid.NewGuid().ToString();
            email = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            string? name = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name))
                throw new UnauthorizedAccessException("User is not authenticated");
            
            _cache.Set(email,token,TimeSpan.FromMinutes(30));
       
        }
        else if (reason == "forgotPassword")
        {
            _cache.Set(email,forgotToken,TimeSpan.FromMinutes(30));
        }

        
      using (SmtpClient client = new SmtpClient(_opt.Host, _opt.Port))
      {
          client.Credentials = new NetworkCredential(_opt.Sender,_opt.Password);
          client.EnableSsl = true;
          client.UseDefaultCredentials = false;
          MailAddress to = new MailAddress(email);
          MailMessage message = new(_from,to);
          message.Subject = "Job Recruitment";
            message.Body = reason == "confirmation" 
              ? $"Salam aleykum, confirmation token: {token}" 
              : $"Salam aleykum, forgot token: {forgotToken}";
        
          await client.SendMailAsync(message);
      }
    }

}