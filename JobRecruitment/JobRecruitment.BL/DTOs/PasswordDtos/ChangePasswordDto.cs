namespace JobRecruitment.BL.DTOs.PasswordDtos;

public class ChangePasswordDto
{
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}