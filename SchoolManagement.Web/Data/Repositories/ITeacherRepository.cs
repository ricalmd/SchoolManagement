using System.Collections.Generic;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        Teacher GetTeacher(int disciplineId, string userId);

        ICollection<TeacherWithDisciplineViewModel> GetUsersFromTeachers();
    }
}
