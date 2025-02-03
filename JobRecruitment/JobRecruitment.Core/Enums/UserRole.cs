namespace JobRecruitment.Core.Enums;

public enum UserRole
{
    Employer = 1,
    Candidate = 2,
    Admin = Employer | Candidate
}