using JobRecruitment.BL.DTOs.CandidateJobOfferDtos;
using JobRecruitment.BL.DTOs.UserDtos;
using JobRecruitment.Core.Entities;

namespace JobRecruitment.BL.Services.Interfaces;

public interface ICandidateJobOfferService
{
    Task CreateCandidateJobOffer(CandidateJobOfferCreateDto dto,string uploadPath);
    Task HardDeleteCandidateJobOffer(int id);
    Task SoftDeleteCandidateJobOffer(int id);
    Task RestoreCandidateJobOffer(int id);
    Task UpdateCandidateJobOffer(int id,CandidateJobOfferUpdateDto dto,string? uploadPath);
    Task<CandidateJobOfferGetDto?> GetByIdCandidateJobOffer(int id);
    Task<IEnumerable<CandidateJobOfferGetDto>> GetAllCandidateJobOffers();
    Task<IEnumerable<CandidateJobOffer>> GetAllCandidateJobOffersForAdmin();
}