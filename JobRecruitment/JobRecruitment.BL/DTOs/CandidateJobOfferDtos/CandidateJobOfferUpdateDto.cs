using JobRecruitment.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace JobRecruitment.BL.DTOs.CandidateJobOfferDtos;

public class CandidateJobOfferUpdateDto
{
    public int JobOfferId { get; set; }  
    public IFormFile? Resume { get; set; }  
}