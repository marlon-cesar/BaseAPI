namespace BaseAPI.Repository.Contexts
{
    using BaseAPI.Repository.Config;
    using BaseAPI.Repository.Mapping.Common;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class MainDbContext : DbContext
    {
        public MainDbContext() { 
        }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql("MainDbConnection");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddAssemblyConfiguration<IEntityMap>();
            modelBuilder.UseCollation("Portuguese_Brazil.1252");

            base.OnModelCreating(modelBuilder);
        }
    }
}
