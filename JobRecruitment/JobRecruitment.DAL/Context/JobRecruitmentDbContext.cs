using JobRecruitment.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobRecruitment.DAL.Context;

public class JobRecruitmentDbContext:IdentityDbContext<User>
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Employer> Employers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<JobOffer> JobOffers { get; set; }
    
    public JobRecruitmentDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(JobRecruitmentDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}