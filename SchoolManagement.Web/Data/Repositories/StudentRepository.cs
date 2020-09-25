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
                var items = _context.Classifications.Where(c => c.StudentId == student.Id);

                _context.Students.Remove(student);

                foreach (var item in items)
                {
                    _context.Classifications.Remove(item);
                }

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

        public List<User> GetStudents(int itemClass)
        {
            return _context.Students
                .Where(s => s.ClassId.Equals(itemClass))
                .Join(_context.Classes, s => s.ClassId, c => c.Id,
                (s, c) => new { students = s, classes = c })
                .Join(_context.Users, s => s.students.User.Id, u => u.Id,
                (s, u) => new { students = s, users = u }).AsNoTracking()
                .Select(x => x.users).ToList();
        }

        public Student GetStudent(int classId, string userId)
        {
            return _context.Students
                .Where(u => u.User.Id.Equals(userId) && u.Class.Id.Equals(classId)).FirstOrDefault();
        }
    }
}
