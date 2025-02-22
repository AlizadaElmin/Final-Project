using JobRecruitment.BL.DTOs.JobOfferDtos;
using JobRecruitment.Core.Entities;

namespace JobRecruitment.BL.Services.Interfaces;

public interface IJobOfferService
{
    Task CreateJobOffer(JobOfferCreateDto dto);
    Task HardDeleteJobOffer(int id);
    Task SoftDeleteJobOffer(int id);
    Task RestoreJobOffer(int id);
    Task UpdateJobOffer(int id,JobOfferUpdateDto dto);
    Task<JobOfferGetDto?> GetByIdJobOffer(int id);
    Task<IEnumerable<JobOfferGetDto>> GetAllJobOffers();
    Task<IEnumerable<JobOfferGetDto>> GetFilteredJobOffers(string category);
}