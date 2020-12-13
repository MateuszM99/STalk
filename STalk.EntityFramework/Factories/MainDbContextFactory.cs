using EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EntityFramework.Factories
{
    class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public MainDbContextFactory()
        {

        }
        public MainDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(Directory.GetCurrentDirectory() + "../../STalk.Api/appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<MainDbContext>();
            var connectionString = configuration.GetConnectionString("STalkConnectionString");
            builder.UseSqlServer();

            return new MainDbContext(builder.Options);
        }
    }
}
