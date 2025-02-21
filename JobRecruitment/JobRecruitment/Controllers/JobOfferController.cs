using JobRecruitment.BL.DTOs.CategoryDtos;
using JobRecruitment.BL.DTOs.JobOfferDtos;
using JobRecruitment.BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobRecruitment.Controllers;
[Route("api/[controller]")]
[ApiController] 
public class JobOfferController(IJobOfferService _service) : ControllerBase
{ 
    [HttpPost]
    public async Task<IActionResult> Create(JobOfferCreateDto dto)
    { 
        await _service.CreateJobOffer(dto); 
           return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, JobOfferUpdateDto dto)
    {
        await _service.UpdateJobOffer(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        await _service.SoftDeleteJobOffer(id);
        return Ok();
    }

    [HttpPut("restore/{id}")]

    public async Task<IActionResult> Restore(int id)
    { 
        await _service.RestoreJobOffer(id); 
        return Ok();
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> HardDelete(int id)
    {
        await _service.HardDeleteJobOffer(id);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var jobOffer = await _service.GetByIdJobOffer(id);
        if (jobOffer == null) return NotFound("Job offer not found");
        return Ok(jobOffer);
    }
    [HttpGet] 
    public async Task<IActionResult> GetAll() 
    { 
        var jobOffers = await _service.GetAllJobOffers(); 
        return Ok(jobOffers);
    }
}
