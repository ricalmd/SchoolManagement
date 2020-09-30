using System.Collections.Generic;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface IDisciplineRepository : IGenericRepository<Discipline>
    {
        List<Discipline> GetAllDisciplines();

        List<Discipline> GetDisciplines(int id);

        List<Discipline> GetDisciplinesFromClass(int id);

        List<ClassWithDisciplinesViewModel> GetDisciplinesFromTeacher(string id, int disciplineId);
    }
}
