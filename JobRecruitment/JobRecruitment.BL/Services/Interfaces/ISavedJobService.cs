using JobRecruitment.BL.DTOs.CategoryDtos;
using JobRecruitment.BL.DTOs.JobOfferDtos;
using JobRecruitment.BL.DTOs.SavedJobDtos;
using JobRecruitment.Core.Entities;

namespace JobRecruitment.BL.Services.Interfaces;

public interface ISavedJobService
{
    Task CreateSavedJob(SavedJobCreateDto dto);
    Task HardDeleteSavedJob(int id);
    Task SoftDeleteSavedJob(int id);
    Task RestoreSavedJob(int id);
    Task UpdateSavedJob(int id,SavedJobUpdateDto dto);
    Task<SavedJobGetDto?> GetByIdSavedJob(int id);
    Task<IEnumerable<SavedJobGetDto>> GetAllSavedJobs();
    Task<IEnumerable<SavedJob>> GetAllSavedJobsForAdmin();
}