using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Configurations
{
    public class ContactListConfiguration : IEntityTypeConfiguration<ContactList>
    {
        public void Configure(EntityTypeBuilder<ContactList> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithOne(x => x.ContactList).HasForeignKey<ContactList>(x => x.UserId);
            builder.HasMany(x => x.Contacts).WithOne(x => x.ContactList).HasForeignKey(x => x.ContactListId);
        }
    }
}
