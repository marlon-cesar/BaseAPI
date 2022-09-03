namespace BaseAPI.Repository.Contexts
{
    using Microsoft.EntityFrameworkCore;

    using BaseAPI.Repository.Config;
    using BaseAPI.Repository.Mapping.Common;

    public class MainDbContext : DbContext
    {
        public MainDbContext()
        {
        }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("MainDbConnection");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddAssemblyConfiguration<IEntityMap>();
            modelBuilder.UseCollation("Latin1_General_100_CI_AI");

            base.OnModelCreating(modelBuilder);
        }
    }
}
