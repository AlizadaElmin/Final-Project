using JobRecruitment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobRecruitment.DAL.Configurations;

public class CandidateJobOfferConfiguration:IEntityTypeConfiguration<CandidateJobOffer>
{
    public void Configure(EntityTypeBuilder<CandidateJobOffer> builder)
    {
        builder.HasOne(x=>x.Candidate).WithMany(y=>y.AppliedJobs).HasForeignKey(x=>x.CandidateId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x=>x.JobOffer).WithMany(y=>y.Candidates).HasForeignKey(x=>x.JobOfferId).OnDelete(DeleteBehavior.NoAction);
        builder.Property(x=>x.ResumeUrl).IsRequired().HasMaxLength(255);
    }
}