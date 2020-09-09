using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        CourseAndDisciplinesViewModel CourseAndDisciplines(Course course, ICollection<Discipline> disciplines);

        IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboSubjects();

        Task<Course> GetCourseAsync(int id);

        AddDisciplinesViewModel ToAddDisciplinesViewModel(Course course);
    }
}
