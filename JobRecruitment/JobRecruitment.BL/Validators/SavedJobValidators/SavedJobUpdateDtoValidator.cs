using FluentValidation;
using JobRecruitment.BL.DTOs.SavedJobDtos;

namespace JobRecruitment.BL.Validators.SavedJobValidators;

public class SavedJobUpdateDtoValidator: AbstractValidator<SavedJobUpdateDto>
{
    public SavedJobUpdateDtoValidator()
    {
        RuleFor(x => x.JobOfferId)
            .GreaterThan(0).WithMessage("Job Offer ID must be greater than zero.");
    }
}