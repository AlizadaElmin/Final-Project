using AutoMapper;
using JobRecruitment.BL.DTOs.CategoryDtos;
using JobRecruitment.Core.Entities;

namespace JobRecruitment.BL.Profiles;

public class CategoryProfile:Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();
        CreateMap<Category, CategoryGetDto>();
    }
}