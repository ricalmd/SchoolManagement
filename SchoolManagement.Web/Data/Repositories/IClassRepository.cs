using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        List<Class> GetClassesFromCourse(int idCourse);

        List<Class> GetClassesFromTeacher(string id);

        List<Class> GetClassesFromUser(string id);

        IEnumerable<SelectListItem> GetComboCourses();
    }
}
