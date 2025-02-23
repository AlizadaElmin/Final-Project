using FluentValidation;
using JobRecruitment.BL.DTOs.PasswordDtos;
using JobRecruitment.BL.DTOs.UserDtos;

namespace JobRecruitment.BL.Validators.PasswordValidators;

public class ResetPasswordDtoValidator: AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordDtoValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token gereklidir.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email adresi gereklidir.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Yeni şifre gereklidir.")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Şifre onayı gereklidir.")
            .Equal(x => x.NewPassword).WithMessage("Şifreler eşleşmiyor.");
    }
}