using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        private readonly DataContext _context;

        public SubjectRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Subjects.Include(s => s.User);
        }

        public IQueryable<Subject> GetSubjectsWithCourse(IQueryable<CourseWithSubjects> cws)
        {
            return _context.Subjects.Where(s => s.Id.Equals(cws.Select(c => c.SubjectId).FirstOrDefault()));

            /*return _context.Subjects
                .SelectMany(s => s.SubjectsAndCourses, (subject, list) => new { subject, list })
                .Where(lists => lists.list.SubjectId == lists.subject.Id);*/
        }
    }
}
