using System.Linq;
using SchoolManagement.Data.Entities;

namespace SchoolManagement.Data
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        IQueryable GetAllWithUsers();
    }
}
