using JobRecruitment.BL.DTOs.CategoryDtos;
using JobRecruitment.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobRecruitment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService _service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryCreateDto createDto)
    {
        await _service.CreateCategory(createDto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id,CategoryUpdateDto updateDto)
    {
        await _service.UpdateCategory(id, updateDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteCategory(int id)
    {
        await _service.SoftDeleteCategory(id);
        return Ok();
    }

    [HttpPut("restore/{id}")]

    public async Task<IActionResult> Restore(int id)
    {
        await _service.RestoreCategory(id);
        return Ok();
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> HardDeleteCategory(int id)
    {
        await _service.HardDeleteCategory(id);
        return Ok();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdCategory(int id)
    {
        var category = await _service.GetByIdCategory(id);
        if (category == null) return NotFound("Category not found");
        return Ok(category);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _service.GetAllCategories();
        return Ok(categories);
    }
}