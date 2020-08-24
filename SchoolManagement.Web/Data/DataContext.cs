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

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
