using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Configurations
{
    public class ContactListUserConfiguration : IEntityTypeConfiguration<ContactListUser>
    {
        public void Configure(EntityTypeBuilder<ContactListUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ContactList).WithMany(x => x.Contacts).HasForeignKey(x => x.ContactListId);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}
