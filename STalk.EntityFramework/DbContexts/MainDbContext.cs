using Domain.Models;
using EntityFramework.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.DbContexts
{
    public class MainDbContext : IdentityDbContext<User>
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }
        //Nowa migracja - Add-Migration {Nazwa} -OutputDir Migrations
        //Update bazki  - Update-Database

        //public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<AddToContactRequest> AddToContactRequests { get; set; }
        public DbSet<ContactList> ContactLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ConversationConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FileConfiguration());
            modelBuilder.ApplyConfiguration(new ContactListConfiguration());
            modelBuilder.ApplyConfiguration(new ContactListUserConfiguration());
            modelBuilder.ApplyConfiguration(new AddToContactRequestConfiguration());
        }
    }
}
