using JobRecruitment.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace JobRecruitment.Core.Entities;

public class User:IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}