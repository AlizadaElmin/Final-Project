using System.Linq.Expressions;
using AutoMapper;
using JobRecruitment.BL.DTOs.JobOfferDtos;
using JobRecruitment.BL.Services.Interfaces;
using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Enums;
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

    private IQueryable<JobOfferGetDto> getQueryable()
    {
        return _jobOfferRepository.GetQuery(x => new JobOfferGetDto
        {
            Name = x.Name,
            Description = x.Description,
            CategoryId = x.CategoryId,
            Category = x.Category.Name,
            EmployerId = x.EmployerId,
            Candidates = x.Candidates.ToList(),
            SavedByUsers = x.SavedByUsers.ToList()
        },true,false);
    }
    
    public async Task<IEnumerable<JobOfferGetDto>> GetFilteredJobOffers(string category,int? minSalary,int? maxSalary )
    {
       var query = getQueryable();
        if (!String.IsNullOrWhiteSpace(category))
        {
            query = query.Where(x => x.Category == category && x.Status == JobOfferStatus.Active);
        }

        if (minSalary.HasValue)
        {
            query = query.Where(x=>x.MinSalary >= minSalary);
        }

        if (maxSalary.HasValue)
        {
            query = query.Where(x=>x.MaxSalary <= maxSalary);
        }
        return await query.ToListAsync();
    }
    
    public async Task<IEnumerable<JobOfferGetDto>> GetSearchedJobOffers(string name)
    {     
        var query = getQueryable();
       
        if (!String.IsNullOrWhiteSpace(name))
        {
            query = query.Where(x => (x.Name.ToLower().Contains(name.ToLower()) || name.ToLower().Contains(x.Name.ToLower())) && x.Status == JobOfferStatus.Active);
        }

        return await query.ToListAsync();
    }

    public async Task UpdateJobOfferStatusAsync(int jobOfferId)
    {
        var jobOffer = await _jobOfferRepository.GetByIdAsync(jobOfferId);

        if (jobOffer != null)
        {
            if (jobOffer.ExpiryDate < DateTime.Now)
            {
                jobOffer.Status = JobOfferStatus.Expired;
            }
            await _jobOfferRepository.SaveAsync();
        }
    }
}