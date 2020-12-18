using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Configurations
{
    public class AddToContactRequestConfiguration : IEntityTypeConfiguration<AddToContactRequest>
    {
        public void Configure(EntityTypeBuilder<AddToContactRequest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.UserTo).WithMany(x => x.AddToContactRequests).HasForeignKey(x => x.UserToId);
        }
    }
}
