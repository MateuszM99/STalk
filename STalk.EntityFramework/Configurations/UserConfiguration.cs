using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);               
            builder.HasMany(x => x.Messages).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.Conversations).WithMany(x => x.Users).UsingEntity<UserConversation>
            (
                y => y
                      .HasOne(uc => uc.Conversation)
                      .WithMany(c => c.UserConversations)
                      .HasForeignKey(uc => uc.ConversationId),
                y => y
                      .HasOne(uc => uc.User)
                      .WithMany(u => u.UserConversations)
                      .HasForeignKey(uc => uc.UserId),
                y => y.HasKey(z => z.Id)
            );
            builder.HasMany(x => x.Files).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.ContactList).WithOne(x => x.User).HasForeignKey<ContactList>(x => x.UserId);
        }
    }
}
