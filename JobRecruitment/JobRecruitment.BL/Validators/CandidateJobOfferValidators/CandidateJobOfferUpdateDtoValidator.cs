using FluentValidation;
using JobRecruitment.BL.DTOs.CandidateJobOfferDtos;

namespace JobRecruitment.BL.Validators.UserValidators;

public class CandidateJobOfferUpdateDtoValidator:AbstractValidator<CandidateJobOfferUpdateDto>
{
    public CandidateJobOfferUpdateDtoValidator()
    {
        RuleFor(x => x.JobOfferId)
            .NotNull().NotEmpty().WithMessage("Job offer id is required.")
            .GreaterThan(0).WithMessage("JobOfferId must be a valid ID.");

        RuleFor(x => x.Resume)
            .Must(file => file.Length > 0).WithMessage("Uploaded file cannot be empty.")
            .Must(file => file.ContentType == "application/pdf")
            .WithMessage("Only PDF resumes are allowed.");
    }
}