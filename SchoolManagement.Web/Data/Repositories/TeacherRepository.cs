using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        private readonly DataContext _context;

        public TeacherRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public Teacher GetTeacher(int disciplineId, string userId)
        {
            return _context.Teachers
                .Where(u => u.User.Id.Equals(userId) && u.DisciplineId.Equals(disciplineId)).FirstOrDefault();
        }

        public ICollection<TeacherWithDisciplineViewModel> GetUsersFromTeachers()
        {
            return (from t in _context.Teachers
                    join u in _context.Users on t.User.Id equals u.Id
                    join d in _context.Disciplines on t.DisciplineId equals d.Id
                    select new TeacherWithDisciplineViewModel
                    {
                        TeacherId = t.Id,
                        UserName = u.Name,
                        DisciplineName = d.Name
                    }).AsNoTracking().ToList();
        }
    }
}
