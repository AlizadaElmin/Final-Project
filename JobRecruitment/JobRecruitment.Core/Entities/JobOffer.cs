using JobRecruitment.Core.Entities.Common;

namespace JobRecruitment.Core.Entities;

public class JobOffer:BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string EmployerId { get; set; }
    public Employer Employer { get; set; }
    public ICollection<CandidateJobOffer> Candidates { get; set; } 
    public ICollection<SavedJob> SavedByUsers { get; set; } 
}
