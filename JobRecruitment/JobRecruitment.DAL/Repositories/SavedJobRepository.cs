using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Repositories;
using JobRecruitment.DAL.Context;

namespace JobRecruitment.DAL.Repositories;

public class SavedJobRepository:GenericRepository<SavedJob>,ISavedJobRepository
{
    public SavedJobRepository(JobRecruitmentDbContext _context):base(_context)
    {
    }
}