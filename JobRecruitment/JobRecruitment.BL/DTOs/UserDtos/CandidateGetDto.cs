using JobRecruitment.BL.DTOs.CandidateJobOfferDtos;
using JobRecruitment.BL.DTOs.SavedJobDtos;
using JobRecruitment.Core.Entities;

namespace JobRecruitment.BL.DTOs.UserDtos;

public class CandidateGetDto
{
    public ICollection<CandidateJobOfferGetDto> AppliedJobs { get; set; } 
    public ICollection<SavedJobGetDto> SavedJobs { get; set; } 
}