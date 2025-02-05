namespace JobRecruitment.Core.Entities;

public class Candidate:User
{
    public ICollection<CandidateJobOffer> AppliedJobs { get; set; } 
    public ICollection<SavedJob> SavedJobs { get; set; } 
}