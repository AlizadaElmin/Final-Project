using JobRecruitment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobRecruitment.DAL.Configurations;

public class SavedJobConfiguration:IEntityTypeConfiguration<SavedJob>
{
    public void Configure(EntityTypeBuilder<SavedJob> builder)
    {
        builder.HasOne(x=>x.Candidate).WithMany(y=>y.SavedJobs).HasForeignKey(x=>x.CandidateId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x=>x.JobOffer).WithMany(y=>y.SavedByUsers).HasForeignKey(x=>x.JobOfferId).OnDelete(DeleteBehavior.NoAction);
    }
}