using JobRecruitment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobRecruitment.DAL.Configurations;

public class CategoryConfiguration:IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x=>x.Name).IsRequired().HasMaxLength(64);
        builder.HasMany(x=>x.JobOffers).WithOne(y=>y.Category).HasForeignKey(y=>y.CategoryId);
    }
}