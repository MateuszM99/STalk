using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.DbContexts
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            
        }
        //Nowa migracja - Add-Migration {Nazwa} -OutputDir Migrations


        public DbSet<TestModel> TestModels { get; set; }
    }
}
