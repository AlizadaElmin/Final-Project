using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JobRecruitment.BL.DTOs.Options;
using JobRecruitment.BL.ExternalServices.Interfaces;
using JobRecruitment.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JobRecruitment.BL.ExternalServices.Implements;

public class JwtTokenHandler:IJwtTokenHandler
{
    readonly JwtOptions _opt;
    private UserManager<User> _userManager;
    public JwtTokenHandler(IOptions<JwtOptions> opt,UserManager<User> userManager)
    {
        _opt = opt.Value;
        _userManager = userManager;
    }
    public async Task<string> CreateToken(User user, int hours = 36)
    {
        var roles = await _userManager.GetRolesAsync(user);
        List<Claim> claims = [
            new Claim(ClaimTypes.Name, user.Fullname),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Role, roles[0]),
        ];
           
       
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.SecretKey));
        SigningCredentials cred = new(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken secToken = new(
            issuer: _opt.Issuer,
            audience: _opt.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddHours(hours),
            signingCredentials: cred
        );
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(secToken);
    }
}