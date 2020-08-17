using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public class CourseWithSubjectsRepository : GenericRepository<CourseWithSubjects>, ICourseWithSubjectsRepository
    {
        private readonly DataContext _context;

        public CourseWithSubjectsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<CourseWithSubjects> GetCourseWithSubjects(int idCourse)
        {
            return _context.CoursesWithSubjects.Where(c => c.CourseId == idCourse);
        }

        public CourseWithSubjects ToAddCourseWithSubjects(int id, AddSubjectsViewModel model)
        {
            return new CourseWithSubjects
            {
                CourseId = id,
                SubjectId = model.SubjectId
            };
        }
    }
}
