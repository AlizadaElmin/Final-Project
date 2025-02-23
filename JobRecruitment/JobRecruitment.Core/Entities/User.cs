using JobRecruitment.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace JobRecruitment.Core.Entities;

public class User:IdentityUser
{
    public string Fullname { get; set; }
}