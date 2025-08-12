using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Persistence.EntityConfigurations;

public class ProjectTaskUserConfiguration : IEntityTypeConfiguration<ProjectTaskUser>
{
    public void Configure(EntityTypeBuilder<ProjectTaskUser> builder)
    {
        builder.ToTable("ProjectTaskUsers").HasKey(pt => pt.Id);
        builder.Property(pt => pt.Id).HasColumnName("Id").IsRequired();
        builder.Property(pt => pt.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(pt => pt.ProjectTaskId).HasColumnName("ProjectTaskId").IsRequired();
        builder.Property(pt => pt.ProjectId).HasColumnName("ProjectId").IsRequired();
        builder.Property(pt => pt.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(pt => pt.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(pt => pt.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(pt => pt.ProjectTask);

        builder.HasOne(pt => pt.User);

        builder.HasOne(pt => pt.Project);

        builder.HasQueryFilter(pt => !pt.DeletedDate.HasValue);

    }
}
