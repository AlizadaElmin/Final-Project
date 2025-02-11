using AutoMapper;
using JobRecruitment.BL.DTOs.JobOfferDtos;
using JobRecruitment.Core.Entities;

namespace JobRecruitment.BL.Profiles;

public class JobOfferProfile:Profile    
{
    public JobOfferProfile()
    {
        CreateMap<JobOfferCreateDto, JobOffer>();
        CreateMap<JobOfferUpdateDto, JobOffer>();
    }
}