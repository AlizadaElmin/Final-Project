using JobRecruitment.Core.Entities;

namespace JobRecruitment.BL.DTOs.JobOfferDtos;

public class JobOfferGetDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
    public string EmployerId { get; set; }
    public ICollection<CandidateJobOffer> Candidates { get; set; } 
    public ICollection<SavedJob> SavedByUsers { get; set; }  
}