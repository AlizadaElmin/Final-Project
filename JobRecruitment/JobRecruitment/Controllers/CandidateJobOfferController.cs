using JobRecruitment.BL.DTOs.CategoryDtos;
using JobRecruitment.BL.DTOs.UserDtos;
using JobRecruitment.BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Runtime.InteropServices;
using JobRecruitment.BL.DTOs.CandidateJobOfferDtos;
using JobRecruitment.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace JobRecruitment.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CandidateJobOfferController(ICandidateJobOfferService _service, IWebHostEnvironment _env) : ControllerBase
{ 
    [HttpPost]
    public async Task<IActionResult> Create(CandidateJobOfferCreateDto dto)
    { 
        string destination = _env.WebRootPath;
        await _service.CreateCandidateJobOffer(dto, destination);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCandidateJobOffer(int id, CandidateJobOfferUpdateDto dto)
    {
        string destination = _env.WebRootPath;
        await _service.UpdateCandidateJobOffer(id,dto,destination);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteCandidateJobOffer(int id)
    {
        await _service.SoftDeleteCandidateJobOffer(id);
        return Ok();
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> HardDeleteCandidateJobOffer(int id)
    {
        await _service.HardDeleteCandidateJobOffer(id);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdCandidateJobOffer(int id)
    {
        var offer = await _service.GetByIdCandidateJobOffer(id);
        if (offer == null) return NotFound("Candidate job offer not found");
        return Ok(offer);
    }

    [HttpPut("restore/{id}")]
    public async Task<IActionResult> Restore(int id)
    {
        await _service.RestoreCandidateJobOffer(id);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var offers = await _service.GetAllCandidateJobOffers();
        return Ok(offers);
    }
}
