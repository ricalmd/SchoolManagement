using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data.Entities;

namespace SchoolManagement.Data
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
    }
}
