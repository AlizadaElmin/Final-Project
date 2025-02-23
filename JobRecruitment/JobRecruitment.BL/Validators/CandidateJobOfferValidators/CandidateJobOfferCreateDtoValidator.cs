using FluentValidation;
using JobRecruitment.BL.DTOs.CandidateJobOfferDtos;
using JobRecruitment.BL.DTOs.UserDtos;

namespace JobRecruitment.BL.Validators.CandidateJobOfferValidators;

public class CandidateJobOfferCreateDtoValidator:AbstractValidator<CandidateJobOfferCreateDto>
{
    public CandidateJobOfferCreateDtoValidator()
    {
        RuleFor(x => x.JobOfferId)
            .NotNull().NotEmpty().WithMessage("Job offer id is required.")
            .GreaterThan(0).WithMessage("JobOfferId must be a valid ID.");

        RuleFor(x => x.Resume)
            .NotNull().WithMessage("Resume file is required.")
            .Must(file => file.Length > 0).WithMessage("Uploaded file cannot be empty.")
            .Must(file => file.ContentType == "application/pdf")
            .WithMessage("Only PDF resumes are allowed.");
    }
}