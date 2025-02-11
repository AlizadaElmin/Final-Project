using JobRecruitment.BL.DTOs.CategoryDtos;
using JobRecruitment.Core.Entities;

namespace JobRecruitment.BL.Services.Interfaces;

public interface ICategoryService
{
    Task CreateCategory(CategoryCreateDto dto);
    Task HardDeleteCategory(int id);
    Task SoftDeleteCategory(int id);
    Task RestoreCategory(int id);
    Task UpdateCategory(int id,CategoryUpdateDto dto);
    Task<CategoryGetDto?> GetByIdCategory(int id);
    Task<IEnumerable<CategoryGetDto>> GetAllCategories();
    Task<IEnumerable<Category>> GetAllCategoriesForAdmin();
}