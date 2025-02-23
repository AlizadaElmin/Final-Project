using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Enums;

namespace JobRecruitment.BL.DTOs.JobOfferDtos;

public class JobOfferGetDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Decimal? MinSalary { get; set; }
    public Decimal? MaxSalary { get; set; }
    public DateTime ExpiryDate { get; set; } 
    public JobOfferStatus Status { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
    public string EmployerId { get; set; }
    public ICollection<CandidateJobOffer> Candidates { get; set; } 
    public ICollection<SavedJob> SavedByUsers { get; set; }  
}