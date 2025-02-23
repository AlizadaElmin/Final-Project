using AutoMapper;
using JobRecruitment.BL.DTOs.CandidateJobOfferDtos;
using JobRecruitment.BL.DTOs.UserDtos;
using JobRecruitment.BL.Exceptions.CandidateJobOfferException;
using JobRecruitment.BL.Exceptions.Common;
using JobRecruitment.BL.Extensions;
using JobRecruitment.BL.ExternalServices.Interfaces;
using JobRecruitment.BL.Services.Interfaces;
using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Enums;
using JobRecruitment.Core.Repositories;

namespace JobRecruitment.BL.Services.Implements;

public class CandidateJobOfferService(ICandidateJobOfferRepository _candidateJobOfferRepository,IJobOfferRepository _jobOfferRepository,IMapper _mapper,ICurrentUser _user): ICandidateJobOfferService
{
    private string _userId = _user.GetId().ToString();
    public async Task CreateCandidateJobOffer(CandidateJobOfferCreateDto dto,string uploadPath)
    {
        var jobOffer = _jobOfferRepository.GetByIdAsync(dto.JobOfferId);
        
        if((int)jobOffer.Status != (int)JobOfferStatus.Active)
            throw new JobOfferNotActiveException();
        
        var candidateJobOffer = _mapper.Map<CandidateJobOffer>(dto);
        candidateJobOffer.CandidateId = _userId;
        if (dto.Resume != null)
        {
            if (!dto.Resume.IsValidType("application/pdf"))
                throw new InvalidResumeTypeException();

            if (!dto.Resume.IsValidSize(3))
                throw new InvalidResumeSizeException();
            
            string fileName = await dto.Resume.UploadFileAsync(uploadPath);
            candidateJobOffer.ResumeUrl = Path.Combine("wwwroot",fileName);
        }
      
        await _candidateJobOfferRepository.AddAsync(candidateJobOffer); 
        await _candidateJobOfferRepository.SaveAsync();
    }
    
    public async Task HardDeleteCandidateJobOffer(int id)
    {
        var candidateJobOffer  =await _candidateJobOfferRepository.GetByIdAsync(id, x => new CandidateJobOffer()
        {
            CandidateId = x.CandidateId,
            ResumeUrl = x.ResumeUrl,
            JobOfferId = x.JobOfferId
        });
        if (candidateJobOffer == null)
        {
            throw new NotFoundException<CandidateJobOffer>();
        }
        var path = Path.Combine("wwwroot",candidateJobOffer.ResumeUrl);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        await _candidateJobOfferRepository.DeleteAndSaveAsync(id);
    }

    public async Task SoftDeleteCandidateJobOffer(int id)
    {
        await _candidateJobOfferRepository.SoftDeleteAsync(id);
        await _candidateJobOfferRepository.SaveAsync();
    }

    public async Task RestoreCandidateJobOffer(int id)
    {
        await _candidateJobOfferRepository.ReverseSoftDeleteAsync(id);
        await _candidateJobOfferRepository.SaveAsync();
    }

    public async Task UpdateCandidateJobOffer(int id, CandidateJobOfferUpdateDto dto,string? uploadPath)
    {
        var jobOffer = _jobOfferRepository.GetByIdAsync(dto.JobOfferId);
        
        if((int)jobOffer.Status != (int)JobOfferStatus.Active)
            throw new JobOfferNotActiveException();
        
        var candidateJobOffer = await _candidateJobOfferRepository.GetByIdAsync(id, false);
        if (candidateJobOffer == null)
            throw new NotFoundException<CandidateJobOffer>();

        _mapper.Map(dto, candidateJobOffer);
        candidateJobOffer.CandidateId = _userId;
        if (dto.Resume != null)
        {
            if (!dto.Resume.IsValidType("application/pdf"))
                throw new InvalidResumeTypeException();

            if (!dto.Resume.IsValidSize(3))
                throw new InvalidResumeSizeException();
            
            var path = Path.Combine("wwwroot",candidateJobOffer.ResumeUrl);
            if (File.Exists(path))
                File.Delete(path);
            string fileName = await dto.Resume.UploadFileAsync(uploadPath);
            candidateJobOffer.ResumeUrl = Path.Combine("wwwroot","resumes",fileName);
        }
        await _candidateJobOfferRepository.SaveAsync();
    }

    public async Task<CandidateJobOfferGetDto?> GetByIdCandidateJobOffer(int id)
    {
        var candidateJobOffer = await _candidateJobOfferRepository.GetByIdAsync(id, x => new CandidateJobOfferGetDto()
        {
            CandidateId = x.CandidateId,
            ResumeUrl = x.ResumeUrl,
            JobOfferId = x.JobOfferId
        });
        if (candidateJobOffer == null)
            throw new NotFoundException<CandidateJobOffer>();
        return candidateJobOffer;
    }

    public async Task<IEnumerable<CandidateJobOfferGetDto>> GetAllCandidateJobOffers()
    {
        var candidateJobOffers = await _candidateJobOfferRepository.GetAllAsync(x => new CandidateJobOfferGetDto()
        {
            CandidateId = x.CandidateId,
            ResumeUrl = x.ResumeUrl,
            JobOfferId = x.JobOfferId
        }, true,true);
        return candidateJobOffers;
    }
}