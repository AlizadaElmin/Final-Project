using JobRecruitment.Core.Entities.Common;

namespace JobRecruitment.Core.Entities;

public class CandidateJobOffer:BaseEntity
{
    public string CandidateId { get; set; }
    public Candidate Candidate { get; set; }
    
    public int JobOfferId { get; set; }
    public JobOffer JobOffer { get; set; }
    public string ResumeUrl { get; set; }
}