using Kontest.Data.Extenstions;
using Kontest.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kontest.Data.Configurations
{
    public class UserOrganizationConfiguration : DbEntityConfiguration<UserOrganization>
    {
        public override void Configure(EntityTypeBuilder<UserOrganization> entity)
        {
            entity.HasKey(uo => uo.Id);
            entity.HasOne(x => x.User).WithMany(x => x.UserOrganizations).HasForeignKey(x => x.UserId);
            entity.HasOne(x => x.Organization).WithMany(x => x.UserOrganizations).HasForeignKey(x => x.OrganizationId);
        }
    }
}
