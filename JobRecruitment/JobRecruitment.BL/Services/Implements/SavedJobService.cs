using AutoMapper;
using JobRecruitment.BL.DTOs.JobOfferDtos;
using JobRecruitment.BL.DTOs.SavedJobDtos;
using JobRecruitment.BL.ExternalServices.Interfaces;
using JobRecruitment.BL.Services.Interfaces;
using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Repositories;

namespace JobRecruitment.BL.Services.Implements;

public class SavedJobService(ISavedJobRepository _savedJobRepository,IMapper _mapper,ICurrentUser _user):ISavedJobService
{
    public async Task CreateSavedJob(SavedJobCreateDto dto)
    {
        var savedJob = _mapper.Map<SavedJob>(dto);
        savedJob.CandidateId = _user.GetId();
        await _savedJobRepository.AddAsync(savedJob); 
        await _savedJobRepository.SaveAsync();
    }

    public async Task HardDeleteSavedJob(int id)
    {
        await _savedJobRepository.DeleteAndSaveAsync(id);
    }

    public async Task SoftDeleteSavedJob(int id)
    {
        await _savedJobRepository.SoftDeleteAsync(id);
        await _savedJobRepository.SaveAsync();
    }

    public async Task RestoreSavedJob(int id)
    {
        await _savedJobRepository.ReverseSoftDeleteAsync(id);
        await _savedJobRepository.SaveAsync();
    }

    public async Task UpdateSavedJob(int id, SavedJobUpdateDto dto)
    {
        var savedJob = await _savedJobRepository.GetByIdAsync(id, false);
        if (savedJob == null)
            throw new Exception("Saved job not found");  //exception

        _mapper.Map(dto, savedJob);
        savedJob.CandidateId = _user.GetId();
        await _savedJobRepository.SaveAsync();
    }

    public async Task<SavedJobGetDto?> GetByIdSavedJob(int id)
    {
        var savedJob = await _savedJobRepository.GetByIdAsync(id, x => new SavedJobGetDto()
        {
            CandidateId = x.CandidateId,
            JobOfferId = x.JobOfferId,
        });
        if (savedJob == null)
            throw new Exception("Saved job not found"); //exception
        return savedJob;
    }

    public async Task<IEnumerable<SavedJobGetDto>> GetAllSavedJobs()
    {
        var savedJob = await _savedJobRepository.GetAllAsync(x => new SavedJobGetDto()
        {
            CandidateId = x.CandidateId,
            JobOfferId = x.JobOfferId,
        },true,true);
        if (savedJob == null)
            throw new Exception("Saved job not found"); //exception
        return savedJob;
    }

    public async Task<IEnumerable<SavedJob>> GetAllSavedJobsForAdmin()
    {
        return await _savedJobRepository.GetAllAsync(x => x,true);
    }
}