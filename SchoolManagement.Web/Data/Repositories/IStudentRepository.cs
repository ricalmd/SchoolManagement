using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task DeleteStudentAsync(Student student);

        IEnumerable<SelectListItem> GetComboClasses();

        List<User> GetStudents(
            IQueryable<Class> classes,
            IQueryable<Student> students,
            IQueryable<User> users,
            int itemClass);

        IQueryable<Student> GetStudentAsync(int classId, string userId);
    }
}
