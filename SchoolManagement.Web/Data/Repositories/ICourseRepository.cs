using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        CourseAndDisciplinesViewModel CourseAndDisciplines(Course course, ICollection<Discipline> disciplines);

        Course GetAllWithCourse(int id);

        IEnumerable<SelectListItem> GetComboDisciplines();

        Task<Course> GetCourseAsync(int id);

        string GetCourseByStudent(int studentId);

        AddDisciplinesViewModel ToAddDisciplinesViewModel(Course course);
    }
}
