using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// It receives from the controller, as parameters, an object of the Course class and an ICollection of
        /// disciplines so that an object of the CourseAndDisciplinesViewModel class is generated.
        /// </summary>
        /// <param name="course"></param>
        /// <param name="disciplines"></param>
        /// <returns>CourseAndDisciplinesViewModel</returns>
        public CourseAndDisciplinesViewModel CourseAndDisciplines(Course course, ICollection<Discipline> disciplines)
        {
            return new CourseAndDisciplinesViewModel
            {
                Id = course.Id,
                Name = course.Name,
                QNQ = course.QNQ,
                QEQ = course.QEQ,
                Profile = course.Profile,
                Reference = course.Reference,
                Disciplines = disciplines.ToList()
            };
        }

        public Course GetAllWithCourse(int id)
        {
            return _context.Courses.Where(c => c.Id.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<SelectListItem> GetComboDisciplines()
        {
            var list = _context.Disciplines.Select(d => new SelectListItem 
            {
                Text = d.Name,
                Value = d.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem 
            {
                Text = "(Selecionar disciplina)",
                Value = "0"
            });

            return list;
        }

        public async Task<Course> GetCourseAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public AddDisciplinesViewModel ToAddDisciplinesViewModel(Course course)
        {
            return new AddDisciplinesViewModel
            {
                Discipline = GetComboDisciplines(),
                Id = course.Id,
                Name = course.Name,
                Profile = course.Profile,
                QEQ = course.QEQ,
                QNQ = course.QNQ,
                Reference = course.Reference,
                User = course.User
            };
        }
    }
}

