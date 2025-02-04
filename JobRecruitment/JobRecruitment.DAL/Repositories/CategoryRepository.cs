using JobRecruitment.Core.Entities;
using JobRecruitment.Core.Repositories;
using JobRecruitment.DAL.Context;

namespace JobRecruitment.DAL.Repositories;

public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
{
    public CategoryRepository(JobRecruitmentDbContext _context):base(_context)
    {
        
    }
}