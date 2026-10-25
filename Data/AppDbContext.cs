using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data
{
    public class AppDbContext:DbContext
    {
        public virtual DbSet<Student> Students {  get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json")
        //        .Build();
        //    optionsBuilder.UseSqlServer(config.GetConnectionString("constr"));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
