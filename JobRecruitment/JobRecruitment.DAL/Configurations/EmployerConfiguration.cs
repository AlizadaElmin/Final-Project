using JobRecruitment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobRecruitment.DAL.Configurations;

public class EmployerConfiguration:IEntityTypeConfiguration<Employer>
{
    public void Configure(EntityTypeBuilder<Employer> builder)
    {
        builder.Property(x=>x.CompanyName).IsRequired().HasMaxLength(64);
        builder.HasMany(x=>x.PostedJobs).WithOne(x=>x.Employer).HasForeignKey(x=>x.EmployerId);
    }
}