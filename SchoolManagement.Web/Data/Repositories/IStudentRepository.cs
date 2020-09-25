using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task DeleteStudentAsync(Student student);

        IEnumerable<SelectListItem> GetComboClasses();

        List<User> GetStudents(int itemClass);

        Student GetStudent(int classId, string userId);
    }
}
