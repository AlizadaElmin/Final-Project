using JobRecruitment.Core.Entities.Common;

namespace JobRecruitment.Core.Entities;

public class SavedJob:BaseEntity
{
        public string CandidateId { get; set; } 
        public Candidate Candidate { get; set; }  
        public int JobOfferId { get; set; }
        public JobOffer JobOffer { get; set; } 
}