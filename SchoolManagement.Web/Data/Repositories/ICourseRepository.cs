using System.Linq;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        IQueryable GetAllWithUsers();

        IQueryable TestRepos();
    }
}
