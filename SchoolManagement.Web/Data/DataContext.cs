using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseWithDiscipline> CourseWithDisciplines { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Classification> Classifications { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
