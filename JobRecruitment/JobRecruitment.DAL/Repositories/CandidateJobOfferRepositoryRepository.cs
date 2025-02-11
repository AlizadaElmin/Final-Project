using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Repositories;
using JobRecruitment.DAL.Context;

namespace JobRecruitment.DAL.Repositories;

public class CandidateJobOfferRepositoryRepository:GenericRepository<CandidateJobOffer>,ICandidateJobOfferRepository
{
    public CandidateJobOfferRepositoryRepository(JobRecruitmentDbContext _context):base(_context)
    {
    }
}