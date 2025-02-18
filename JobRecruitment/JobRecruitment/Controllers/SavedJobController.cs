using JobRecruitment.BL.DTOs.JobOfferDtos;
using JobRecruitment.BL.DTOs.SavedJobDtos;
using JobRecruitment.BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobRecruitment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedJobController(ISavedJobService _service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(SavedJobCreateDto dto)
        {
            await _service.CreateSavedJob(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SavedJobUpdateDto dto)
        {
            await _service.UpdateSavedJob(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            await _service.SoftDeleteSavedJob(id);
            return Ok();
        }

        [HttpPut("restore/{id}")]

        public async Task<IActionResult> Restore(int id)
        {
            await _service.RestoreSavedJob(id);
            return Ok();
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> HardDelete(int id)
        {
            await _service.HardDeleteSavedJob(id);
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var savedJob = await _service.GetByIdSavedJob(id);
            if (savedJob == null) return NotFound("Job offer not found");
            return Ok(savedJob);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var savedJobs = await _service.GetAllSavedJobs();
            return Ok(savedJobs);
        }
    }
}