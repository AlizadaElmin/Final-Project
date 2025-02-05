using FluentValidation;
using JobRecruitment.BL.DTOs.UserDtos;

namespace JobRecruitment.BL.Validators.UserValidators;

public class RegisterDtoValidator: AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(64).WithMessage("Username cannot exceed 64 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.Firstname)
            .NotEmpty().WithMessage("Firstname is required.")
            .MaximumLength(16).WithMessage("Firstname cannot exceed 16 characters.");

        RuleFor(x => x.Lastname)
            .NotEmpty().WithMessage("Lastname is required.")
            .MaximumLength(16).WithMessage("Lastname cannot exceed 16 characters.");

        RuleFor(x => x.IsMale)
            .NotNull().WithMessage("Gender selection is required.");
    }
   
}