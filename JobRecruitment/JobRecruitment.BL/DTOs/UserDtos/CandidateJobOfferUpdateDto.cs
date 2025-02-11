using Microsoft.AspNetCore.Http;

namespace JobRecruitment.BL.DTOs.UserDtos;

public class CandidateJobOfferUpdateDto
{
    public string CandidateId { get; set; } 
    public int JobOfferId { get; set; }  
    public IFormFile? Resume { get; set; }  
}