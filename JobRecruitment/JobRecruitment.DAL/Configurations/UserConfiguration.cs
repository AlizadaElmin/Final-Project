using JobRecruitment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobRecruitment.DAL.Configurations;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x=>x.FirstName).IsRequired().HasMaxLength(16);
        builder.Property(x=>x.LastName).IsRequired().HasMaxLength(16);
    }
}