using Microsoft.AspNetCore.Http;

namespace JobRecruitment.BL.DTOs.UserDtos;

public class CandidateJobOfferCreateDto
{
    public string CandidateId { get; set; } 
    public int JobOfferId { get; set; }  
    public IFormFile Resume { get; set; }  
}