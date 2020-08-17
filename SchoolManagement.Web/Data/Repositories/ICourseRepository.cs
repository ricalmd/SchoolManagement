using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboSubjects();

        AddSubjectsViewModel ToAddSubjectsViewModel(Course course);
    }
}
