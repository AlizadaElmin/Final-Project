namespace JobRecruitment.BL.DTOs.UserDtos;

public class ResetPasswordDto
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string NewPassword { get; set; }
    
    public string ConfirmPassword { get; set; }
}