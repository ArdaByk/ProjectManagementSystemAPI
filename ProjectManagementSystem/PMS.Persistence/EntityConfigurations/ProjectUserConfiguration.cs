using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Persistence.EntityConfigurations;

public class ProjectUserConfiguration : IEntityTypeConfiguration<ProjectUser>
{
    public void Configure(EntityTypeBuilder<ProjectUser> builder)
    {
        builder.ToTable("ProjectUser").HasKey(p => p.Id);
        builder.ToTable(tb => tb.HasTrigger("trg_Update_MemberCount"));
        builder.Property(pu => pu.Id).HasColumnName("Id").IsRequired();
        builder.Property(pu => pu.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(pu => pu.ProjectId).HasColumnName("ProjectId").IsRequired();
        builder.Property(pu => pu.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(pu => pu.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(pu => pu.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(pu => pu.Project);

        builder.HasOne(pu => pu.User);

        builder.HasQueryFilter(pu => !pu.DeletedDate.HasValue);

    }
}
