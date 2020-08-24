using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable GetAllWithUsers()
        {
            return _context.Courses.Include(c => c.User);
        }

        public IEnumerable<SelectListItem> GetComboSubjects()
        {
            var list = _context.Disciplines.Select(s => new SelectListItem 
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem 
            {
                Text = "(Selecionar disciplina)",
                Value = "0"
            });

            return list;
        }

        public AddDisciplinesViewModel ToAddDisciplinesViewModel(Course course)
        {
            return new AddDisciplinesViewModel
            {
                Discipline = GetComboSubjects(),
                Id = course.Id,
                Name = course.Name,
                Profile = course.Profile,
                QEQ = course.QEQ,
                QNQ = course.QNQ,
                Reference = course.Reference,
                User = course.User
            };
        }

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
                Disciplines = disciplines
            };
        }
    }
}

