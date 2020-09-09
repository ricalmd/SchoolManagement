using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboCourses();
    }
}
