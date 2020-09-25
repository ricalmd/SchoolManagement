using System.Linq;
using SchoolManagement.Web.Data.Entities;

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
    }
}
