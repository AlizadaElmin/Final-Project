using AutoMapper;
using JobRecruitment.BL.DTOs.CategoryDtos;
using JobRecruitment.BL.Services.Interfaces;
using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Repositories;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

namespace JobRecruitment.BL.Services.Implements;

public class CategoryService(ICategoryRepository _categoryRepository,IMapper _mapper): ICategoryService
{
    public async Task CreateCategory(CategoryCreateDto dto)
    {
        Category category = _mapper.Map<Category>(dto);
        await _categoryRepository.AddAsync(category); 
        await _categoryRepository.SaveAsync();
    }

    public async Task SoftDeleteCategory(int id)
    {
        await _categoryRepository.SoftDeleteAsync(id);
        await _categoryRepository.SaveAsync();
    }

    public async Task RestoreCategory(int id)
    {
        await _categoryRepository.ReverseSoftDeleteAsync(id);
        await _categoryRepository.SaveAsync();
    }

    public async Task HardDeleteCategory(int id)
    {
        await _categoryRepository.DeleteAndSaveAsync(id);
    }

    public async Task UpdateCategory(int id, CategoryUpdateDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(id, false);
        if (category == null)
            throw new Exception("Category not found");  //exception

        _mapper.Map(dto, category);
        await _categoryRepository.SaveAsync();
    }

    public async Task<CategoryGetDto?> GetByIdCategory(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id, x => new CategoryGetDto()
        {
            Name = x.Name,
            JobOffers = x.JobOffers
        });
        if (category == null)
            throw new Exception("Category not found"); //exception
        return category;
    }

    public async Task<IEnumerable<CategoryGetDto>> GetAllCategories()
    {
        var categories = await _categoryRepository.GetAllAsync(x => new CategoryGetDto()
        {
            Name = x.Name,
            JobOffers = x.JobOffers
        }, true,true);
        return categories;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesForAdmin()
    {
        return await _categoryRepository.GetAllAsync(x => x,true);
    }
}