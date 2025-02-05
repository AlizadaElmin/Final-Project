using FluentValidation;
using JobRecruitment.BL.DTOs.UserDtos;

namespace JobRecruitment.BL.Validators.UserValidators;

public class LoginDtoValidator: AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.UsernameOrEmail)
            .NotEmpty().WithMessage("Username or Email is required.")
            .MaximumLength(100).WithMessage("Username or Email must not exceed 100 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}