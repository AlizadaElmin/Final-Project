using AutoMapper;
using JobRecruitment.BL.DTOs.UserDtos;
using JobRecruitment.BL.Exceptions.UserException;
using JobRecruitment.BL.ExternalServices.Interfaces;
using JobRecruitment.BL.Services.Interfaces;
using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace JobRecruitment.BL.Services.Implements;

public class AccountService(UserManager<User> _userManager,SignInManager<User> _signInManager,IMapper _mapper,IJwtTokenHandler _jwtTokenHandler,IEmailService _emailService):IAccoutService
{
    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        if (await _userManager.FindByEmailAsync(dto.Email) != null) throw new Exception("The email address is already taken."); 
        if (await _userManager.FindByNameAsync(dto.Username) != null)throw new Exception("The username is already taken."); 
     
        User user = _mapper.Map<User>(dto);
        var result = await _userManager.CreateAsync(user, dto.Password);
        List<string> errorDescription = new List<string>();
        if (!result.Succeeded)
        {
            foreach(var error in  result.Errors)
            {
                errorDescription.Add(error.Description);
            }
        }
        var role =await _userManager.AddToRoleAsync(user,nameof(UserRole.Candidate));
        if (!role.Succeeded)
        {  
            foreach (var error in result.Errors)
            {
                errorDescription.Add(error.Description);
            }
        }

        await _emailService.SendEmailAsync("confirmation",dto.Email,null);
        return user.UserName;
    } 

    public async Task<string> LoginAsync(LoginDto dto)
    {
        User? user = null;
        if (dto.UsernameOrEmail.Contains("@"))
        {
             user = await _userManager.FindByEmailAsync(dto.UsernameOrEmail);
        }
        else
        {
             user =await _userManager.FindByNameAsync(dto.UsernameOrEmail);
        }
        if(user==null) throw new UserNotFoundException("User not found.");
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password,false);
        if (!result.Succeeded)
        {
            if(result.IsLockedOut) throw new AccountLockedException(user.LockoutEnd!.Value);
            if(result.IsNotAllowed) throw new LoginFailedException("Username or password is incorrect.");
        }

        var token =  await _jwtTokenHandler.CreateToken(user, 36);
        return token ;
    }
}
