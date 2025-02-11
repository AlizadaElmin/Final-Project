namespace JobRecruitment.BL.DTOs.UserDtos;

public class CandidateJobOfferGetDto
{
    public string CandidateId { get; set; } 
    public int JobOfferId { get; set; }  
    public string ResumeUrl { get; set; }  
}