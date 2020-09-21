using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteStudentAsync(Student student)
        {
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<SelectListItem> GetComboClasses()
        {
            var list = _context.Classes.Select(c => new SelectListItem
            {
                Text = c.NameClass,
                Value = c.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Selecionar turma)",
                Value = "0"
            });

            return list;
        }

        public List<User> GetStudents(
            IQueryable<Class> classes, 
            IQueryable<Student> students,
            IQueryable<User> users, 
            int itemClass)
        {
            return students
                .Where(s => s.ClassId.Equals(itemClass))
                .Join(classes, s => s.ClassId, c => c.Id,
                (s, c) => new { students = s, classes = c })
                .Join(users, s => s.students.User.Id, u => u.Id,
                (s, u) => new { students = s, users = u }).AsNoTracking()
                .Select(x => x.users).ToList();
        }

        public IQueryable<Student> GetStudentAsync(int classId, string userId)
        {
            return _context.Students
                .Where(u => u.User.Id.Equals(userId) && u.Class.Id.Equals(classId));
        }
    }
}
