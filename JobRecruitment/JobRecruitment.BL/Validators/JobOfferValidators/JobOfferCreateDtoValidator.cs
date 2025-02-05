using FluentValidation;
using JobRecruitment.BL.DTOs.JobOfferDtos;
using JobRecruitment.Core.Repositories;

namespace JobRecruitment.BL.Validators.JobOfferValidators;

public class JobOfferCreateDtoValidator: AbstractValidator<JobOfferCreateDto>
{
    private readonly IJobOfferRepository _repo;
    public JobOfferCreateDtoValidator(IJobOfferRepository repo)
    {
        _repo = repo;
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Job offer name is required.")
            .Length(3, 64)
            .WithMessage("Job offer name must be between 3 and 64 characters.");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Job offer description is required.")
            .Length(10, 256)
            .WithMessage("Job offer description must be between 10 and 256 characters.");
        
        RuleFor(x => x.CategoryId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Job offer category is required.")
            .GreaterThan(0)
            .WithMessage("CategoryId must be a valid ID.");

        RuleFor(x => x.EmployerId)
            .NotEmpty()
            .WithMessage("EmployerId is required.");
    }
}