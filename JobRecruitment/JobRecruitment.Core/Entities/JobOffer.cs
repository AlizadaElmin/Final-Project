using JobRecruitment.Core.Entities.Common;
using JobRecruitment.Core.Enums;

namespace JobRecruitment.Core.Entities;

public class JobOffer:BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? MinSalary { get; set; } 
    public decimal? MaxSalary { get; set; }
    public DateTime ExpiryDate { get; set; } 
    public JobOfferStatus Status { get; set; } = JobOfferStatus.Active;
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string EmployerId { get; set; }
    public Employer Employer { get; set; }
    public ICollection<CandidateJobOffer> Candidates { get; set; } 
    public ICollection<SavedJob> SavedByUsers { get; set; } 
}
