using System.Security.Claims;
using AutoMapper;
using JobRecruitment.BL.DTOs.UserDtos;
using JobRecruitment.BL.Exceptions.Common;
using JobRecruitment.BL.ExternalServices.Interfaces;
using JobRecruitment.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace JobRecruitment.BL.ExternalServices.Implements;

public class CurrentUser(IHttpContextAccessor _httpContext,
   UserManager<User> _userManager,
    IMapper _mapper) : ICurrentUser
{
    ClaimsPrincipal? User = _httpContext.HttpContext?.User;
    public string GetEmail()
    {
        var value = User.FindFirst(x => x.Type == ClaimTypes.Email)?.Value;
        // if (value is null)
            // throw new UserNotFoundException();
        return value;
    }

    public string GetName()
    {
        var value = User.FindFirst(x => x.Type == ClaimTypes.Name)?.Value;
        if (value is null)
            throw new Exception("User does not exist");
        return value;
    }

    public string GetId()
    {
        var value = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (value is null)
            throw new Exception("User does not exist");
        return value;
    }

    public int GetRole()
    {
        var value = User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;
        if (value is null)
            throw new Exception("User does not exist");
        return Convert.ToInt32(value);
    }

    public async Task<UserGetDto> GetUserAsync()
    {
        string name = GetName();
        var user = await _userManager.FindByNameAsync(name);
        return _mapper.Map<UserGetDto>(user);
    }

    public string GetUserName()
    {
        var value = User.FindFirst(x => x.Type == ClaimTypes.Name)?.Value;
        if (value is null)
            throw new Exception("User does not exist");
        return value;
    }
}