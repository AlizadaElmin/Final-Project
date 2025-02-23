using FluentValidation;
using JobRecruitment.BL.DTOs.SavedJobDtos;

namespace JobRecruitment.BL.Validators.SavedJobValidators;

public class SavedJobCreateDtoValidator:AbstractValidator<SavedJobCreateDto>
{
    public SavedJobCreateDtoValidator()
    {
        RuleFor(x => x.JobOfferId)
            .GreaterThan(0).WithMessage("Job Offer ID must be greater than zero.");
    }
}