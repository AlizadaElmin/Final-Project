using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JobRecruitment.BL.Constants;
using JobRecruitment.BL.DTOs.Options;
using JobRecruitment.BL.ExternalServices.Interfaces;
using JobRecruitment.Core.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JobRecruitment.BL.ExternalServices.Implements;

public class JwtTokenHandler:IJwtTokenHandler
{
    readonly JwtOptions opt;
    public JwtTokenHandler(IOptions<JwtOptions> _opt)
    {
        opt = _opt.Value;
    }
    public string CreateToken(User user, int hours = 36)
    {
        List<Claim> claims = [
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        ];
            // new Claim(ClaimType.Role,user.Role.ToString()),
       
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(opt.SecretKey));
        SigningCredentials cred = new(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken secToken = new(
            issuer: opt.Issuer,
            audience: opt.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddHours(hours),
            signingCredentials: cred
        );
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(secToken);
    }
}