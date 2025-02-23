using AutoMapper;
using JobRecruitment.BL.DTOs.CandidateJobOfferDtos;
using JobRecruitment.BL.DTOs.UserDtos;
using JobRecruitment.Core.Entities;

namespace JobRecruitment.BL.Profiles;

public class CandidateJobOfferProfile:Profile
{
    public CandidateJobOfferProfile()
    {
        CreateMap<CandidateJobOfferCreateDto, CandidateJobOffer>();
        CreateMap<CandidateJobOfferUpdateDto, CandidateJobOffer>();
    }
}