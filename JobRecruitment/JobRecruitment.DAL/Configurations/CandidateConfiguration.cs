using JobRecruitment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobRecruitment.DAL.Configurations;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.Property(x=>x.ResumeUrl).IsRequired().HasMaxLength(256);
        builder.HasMany(x=>x.AppliedJobs).WithMany(x=>x.Candidates);
    }
}