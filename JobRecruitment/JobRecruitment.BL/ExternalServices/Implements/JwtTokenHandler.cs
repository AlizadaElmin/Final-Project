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
    readonly JwtOptions _opt;
    public JwtTokenHandler(IOptions<JwtOptions> opt)
    {
        _opt = opt.Value;
    }
    public string CreateToken(User user, int hours = 36)
    {
        List<Claim> claims = [
            new Claim(ClaimTypes.Name, user.Fullname),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        ];
            // new Claim(ClaimType.Role,user.Role)
       
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