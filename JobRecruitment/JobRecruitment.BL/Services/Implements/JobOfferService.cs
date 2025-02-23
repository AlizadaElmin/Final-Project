using System.Linq.Expressions;
using AutoMapper;
using JobRecruitment.BL.DTOs.CandidateJobOfferDtos;
using JobRecruitment.BL.DTOs.JobOfferDtos;
using JobRecruitment.BL.DTOs.SavedJobDtos;
using JobRecruitment.BL.DTOs.UserDtos;
using JobRecruitment.BL.ExternalServices.Interfaces;
using JobRecruitment.BL.Services.Interfaces;
using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Enums;
using JobRecruitment.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobRecruitment.BL.Services.Implements;

public class JobOfferService(IJobOfferRepository _jobOfferRepository,IMapper _mapper,ICurrentUser _user):IJobOfferService
{
    public async Task CreateJobOffer(JobOfferCreateDto dto)
    {
        var jobOffer = _mapper.Map<JobOffer>(dto);
        jobOffer.EmployerId = _user.GetId();
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
        jobOffer.EmployerId = _user.GetId();
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
            MinSalary = x.MinSalary,
            MaxSalary = x.MaxSalary,
            Status = x.Status,
            Category = x.Category.Name,
            Candidates = x.Candidates.Select(c => new CandidateJobOfferGetDto
            {
                CandidateId = c.CandidateId,
                JobOfferId = c.JobOfferId,
                ResumeUrl = c.ResumeUrl
            }).ToList(),
            SavedByUsers = x.SavedByUsers.Select(s => new SavedJobGetDto
            {
                CandidateId = s.CandidateId,
                JobOfferId = s.JobOfferId,
            })
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
            Candidates = x.Candidates.Select(c => new CandidateJobOfferGetDto
            {
                CandidateId = c.CandidateId,
                JobOfferId = c.JobOfferId
            }).ToList(),
            SavedByUsers = x.SavedByUsers.Select(s => new SavedJobGetDto
            {
                CandidateId = s.CandidateId,
                JobOfferId = s.JobOfferId
            })
        }, true,true);
        return jobOffers;
    }


    private IQueryable<JobOfferGetDto> getQueryable()
    {
        return _jobOfferRepository.GetQuery(x => new JobOfferGetDto
        {
            Name = x.Name,
            Description = x.Description,
            CategoryId = x.CategoryId,
            MinSalary = x.MinSalary,
            MaxSalary = x.MaxSalary,
            ExpiryDate = x.ExpiryDate,
            Status = x.Status,
            Category = x.Category.Name,
            EmployerId = x.EmployerId,
            Candidates = x.Candidates.Select(c => new CandidateJobOfferGetDto
            {
                CandidateId = c.CandidateId,
                JobOfferId = c.JobOfferId
            }).ToList(),
            SavedByUsers = x.SavedByUsers.Select(s => new SavedJobGetDto
            {
                CandidateId = s.CandidateId,
                JobOfferId = s.JobOfferId
            })
        },true,false);
    }
    
    public async Task<IEnumerable<JobOfferGetDto>> GetFilteredJobOffers(string? category,decimal? minSalary,decimal? maxSalary )
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