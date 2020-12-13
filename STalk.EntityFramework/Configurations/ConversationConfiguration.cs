using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Configurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Messages).WithOne(x => x.Conversation).HasForeignKey(x => x.ConversationId);
            builder.HasMany(x => x.Users).WithMany(x => x.Conversations).UsingEntity<UserConversation>
            (
                y => y
                      .HasOne(uc => uc.User)
                      .WithMany(u => u.UserConversations)
                      .HasForeignKey(uc => uc.UserId),
                y => y
                      .HasOne(uc => uc.Conversation)
                      .WithMany(c => c.UserConversations)
                      .HasForeignKey(uc => uc.ConversationId),
                y => y.HasKey(z => z.Id)
            );
        }
    }
}
