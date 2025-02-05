using JobRecruitment.Core.Enums;

namespace JobRecruitment.BL.Extensions;

public static class RoleExtension
{
    public static string GetRole(this UserRole role)
    {
        return role switch
        {
            UserRole.Admin => nameof(UserRole.Admin),
            UserRole.Candidate => nameof(UserRole.Candidate),
            UserRole.Employer => nameof(UserRole.Employer)
        };
    }
}