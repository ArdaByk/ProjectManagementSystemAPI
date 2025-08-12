using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Persistence.EntityConfigurations;

public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.ToTable("ProjectTasks").HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.Title).HasColumnName("Title").IsRequired();
        builder.Property(p => p.Content).HasColumnName("Content").IsRequired();
        builder.Property(p => p.Status).HasColumnName("Status").IsRequired();
        builder.Property(p => p.ProjectId).HasColumnName("ProjectId").IsRequired();
        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(ptu => ptu.Project)
    .WithMany() 
    .HasForeignKey(ptu => ptu.ProjectId)
    .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.Users);

        builder.HasQueryFilter(p => !p.DeletedDate.HasValue);
    }
}
