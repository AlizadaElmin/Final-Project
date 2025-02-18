using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Repositories;
using JobRecruitment.DAL.Context;

namespace JobRecruitment.DAL.Repositories;

public class CandidateJobOfferRepository:GenericRepository<CandidateJobOffer>,ICandidateJobOfferRepository
{
    public CandidateJobOfferRepository(JobRecruitmentDbContext _context):base(_context)
    {
    }
}