using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface IDisciplineRepository : IGenericRepository<Discipline>
    {
        List<Discipline> GetAllDisciplines();

        IQueryable GetAllWithUsers();

        List<Discipline> GetDisciplines(int id);

        List<Discipline> GetDisciplinesFromClass(int id);

        List<Discipline> GetDisciplinesFromTeacher(string id, int disciplineId);
    }
}
