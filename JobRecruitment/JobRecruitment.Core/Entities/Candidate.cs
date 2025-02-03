namespace JobRecruitment.Core.Entities;

public class Candidate:User
{
    public string ResumeUrl { get; set; } 
    public ICollection<JobOffer> AppliedJobs { get; set; } 
    public ICollection<SavedJob> SavedJobs { get; set; } 
}