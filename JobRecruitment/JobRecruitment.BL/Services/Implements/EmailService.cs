using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using JobRecruitment.BL.Constants;
using JobRecruitment.BL.DTOs.Options;
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
    public async Task SendEmail()
    { 
        string? email = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value; 
        string? name = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

      if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name))
          throw new UnauthorizedAccessException("User is not authenticated");
      
      string token = Guid.NewGuid().ToString();
      _cache.Set(name,token,TimeSpan.FromMinutes(30));

      using (SmtpClient client = new SmtpClient(_opt.Host, _opt.Port))
      {
          client.Credentials = new NetworkCredential(_opt.Sender,_opt.Password);
          client.EnableSsl = true;
          client.UseDefaultCredentials = false;
          MailAddress to = new MailAddress(email);
          MailMessage message = new(_from,to);
          message.Subject = "Job Recruitment";
          message.Body="Salam aleykum token: "+token;
          
          await client.SendMailAsync(message);
      }
    }

    public async Task AccountVerify(string userToken)
    {
        string? name = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

        var cacheToken = _cache.Get<string>(name);
        if (string.IsNullOrEmpty(userToken) || string.IsNullOrEmpty(name))
            throw new UnauthorizedAccessException("User is not authenticated or token is missing.");
        
        // if (!(cacheToken != null && cacheToken == userToken))
            //throw new NotFoundException<User>(); //Exception
            
        User? user = await _userManager.FindByNameAsync(name);
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);
        var role = nameof(UserRole.Employer);
        await _userManager.AddToRoleAsync(user, role);
        await _context.SaveChangesAsync();
    }   
}