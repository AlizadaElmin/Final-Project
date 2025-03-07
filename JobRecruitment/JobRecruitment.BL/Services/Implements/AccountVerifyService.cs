using System.Security.Claims;
using JobRecruitment.BL.Exceptions.UserException;
using JobRecruitment.BL.Services.Interfaces;
using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Enums;
using JobRecruitment.DAL.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace JobRecruitment.BL.Services.Implements;

public class AccountVerifyService:IAccountVerifyService
{
    private readonly UserManager<User> _userManager;
    private readonly JobRecruitmentDbContext _context;
    private readonly IMemoryCache _cache;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountVerifyService(JobRecruitmentDbContext context,UserManager<User> userManager,IMemoryCache cache,IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _cache = cache;
        _context = context;
    }
    public async Task AccountVerify(string userToken)
    {
        string? email = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        User user = await _userManager.FindByEmailAsync(email);
        var cacheToken = _cache.Get<string>(email);
        if (string.IsNullOrEmpty(userToken) || string.IsNullOrEmpty(cacheToken))
            throw new TokenVerificationException();
        
        if (cacheToken != userToken)
            throw new TokenVerificationException("Provided token does not match the cached token.");
            
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);
        var role = nameof(UserRole.Employer);
        await _userManager.AddToRoleAsync(user, role);
        await _context.SaveChangesAsync();
    }   
}