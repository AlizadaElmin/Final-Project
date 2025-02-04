using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Repositories;
using JobRecruitment.DAL.Context;

namespace JobRecruitment.DAL.Repositories;

public class JobOfferRepository:GenericRepository<JobOffer>,IJobOfferRepository
{
    public JobOfferRepository(JobRecruitmentDbContext _context):base(_context)
    {
    }
}