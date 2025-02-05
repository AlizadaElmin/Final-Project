using JobRecruitment.Core.Entities;

namespace JobRecruitment.BL.DTOs.UserDtos;

public class CandidateGetDto
{
    public ICollection<CandidateJobOffer> AppliedJobs { get; set; } 
    public ICollection<SavedJob> SavedJobs { get; set; } 
}