using JobRecruitment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobRecruitment.DAL.Configurations;

public class JobOfferConfiguration:IEntityTypeConfiguration<JobOffer>
{
    public void Configure(EntityTypeBuilder<JobOffer> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(256);
        builder.HasOne(x=>x.Category).WithMany(y=>y.JobOffers).HasForeignKey(x=>x.CategoryId);
        builder.HasOne(x=>x.Employer).WithMany(y=>y.PostedJobs).HasForeignKey(x=>x.EmployerId);
        builder.HasMany(x=>x.Candidates).WithMany(y=>y.AppliedJobs);
    }
}