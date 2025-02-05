using FluentValidation;
using JobRecruitment.BL.DTOs.CategoryDtos;
using JobRecruitment.Core.Repositories;

namespace JobRecruitment.BL.Validators.CategoryValidators;

public class CategoryCreateDtoValidator:AbstractValidator<CategoryCreateDto>
{
    private readonly ICategoryRepository _repo;

    public CategoryCreateDtoValidator(ICategoryRepository repo)
    {
        _repo = repo;
        
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(64)
            .WithMessage("Maximum length 64 characters");
    }
}