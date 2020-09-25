using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        Teacher GetTeacher(int disciplineId, string userId);
    }
}
