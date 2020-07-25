using SchoolManagement.Data.Entities;

namespace SchoolManagement.Data
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(DataContext context) : base(context)
        {

        }
    }
}
