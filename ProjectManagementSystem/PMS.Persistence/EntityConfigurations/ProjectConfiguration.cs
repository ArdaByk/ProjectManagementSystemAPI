using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Persistence.EntityConfigurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects").HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.ProjectName).HasColumnName("ProjectName").IsRequired();
        builder.Property(p => p.ProjectDescription).HasColumnName("ProjectDescription");
        builder.Property(p => p.Status).HasColumnName("Status").IsRequired();
        builder.Property(p => p.MemberCount).HasColumnName("MemberCount").IsRequired();
        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

        builder.HasMany(p => p.ProjectTasks);

        builder.HasQueryFilter(p => !p.DeletedDate.HasValue);
    }
}
