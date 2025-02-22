using System.Linq.Expressions;
using AutoMapper;
using JobRecruitment.BL.DTOs.JobOfferDtos;
using JobRecruitment.BL.Services.Interfaces;
using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobRecruitment.BL.Services.Implements;

public class JobOfferService(IJobOfferRepository _jobOfferRepository,IMapper _mapper):IJobOfferService
{
    public async Task CreateJobOffer(JobOfferCreateDto dto)
    {
        var jobOffer = _mapper.Map<JobOffer>(dto);
        await _jobOfferRepository.AddAsync(jobOffer); 
        await _jobOfferRepository.SaveAsync();
    }

    public async Task HardDeleteJobOffer(int id)
    {
        await _jobOfferRepository.DeleteAndSaveAsync(id);
    }

    public async Task SoftDeleteJobOffer(int id)
    {
        await _jobOfferRepository.SoftDeleteAsync(id);
        await _jobOfferRepository.SaveAsync();
    }

    public async Task RestoreJobOffer(int id)
    {
        await _jobOfferRepository.ReverseSoftDeleteAsync(id);
        await _jobOfferRepository.SaveAsync();
    }

    public async Task UpdateJobOffer(int id, JobOfferUpdateDto dto)
    {
        var jobOffer = await _jobOfferRepository.GetByIdAsync(id,false);
        if (jobOffer == null)
            throw new Exception("Job offer not found");  //exception

        _mapper.Map(dto, jobOffer);
        await _jobOfferRepository.SaveAsync();
    }

    public async Task<JobOfferGetDto?> GetByIdJobOffer(int id)
    {
        var jobOffer = await _jobOfferRepository.GetByIdAsync(id, x => new JobOfferGetDto()
        {
            Name = x.Name,
            Description = x.Description,    
            CategoryId = x.CategoryId,
            EmployerId = x.EmployerId,
            Candidates = x.Candidates,
            SavedByUsers = x.SavedByUsers
        });
        if (jobOffer == null)
            throw new Exception("Job Offer not found"); //exception
        return jobOffer;
    }

    public async Task<IEnumerable<JobOfferGetDto>> GetAllJobOffers()
    {
        var jobOffers = await _jobOfferRepository.GetAllAsync(x => new JobOfferGetDto()
        {
            Name = x.Name,
            Description = x.Description,    
            CategoryId = x.CategoryId,
            EmployerId = x.EmployerId,
            Candidates = x.Candidates,
            SavedByUsers = x.SavedByUsers
        }, true,true);
        return jobOffers;
    }

    public async Task<IEnumerable<JobOffer>> GetAllJobOffersForAdmin()
    {
        return  await _jobOfferRepository.GetAllAsync(x => x,true);
    }

    public async Task<IEnumerable<JobOfferGetDto>> GetFilteredJobOffers(string category)
    {
        var query = _jobOfferRepository.GetQuery(x => new JobOfferGetDto
        {
            Name = x.Name,
            Description = x.Description,
            CategoryId = x.CategoryId,
            Category = x.Category.Name,
            EmployerId = x.EmployerId,
            Candidates = x.Candidates.ToList(),
            SavedByUsers = x.SavedByUsers.ToList()
        },true,false);
        if (!String.IsNullOrWhiteSpace(category))
        {
            query = query.Where(x => x.Category == category);
        }

        return await query.ToListAsync();
    }
}