using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Helpers
{
    public class QueryHelper : IQueryHelper
    {
        public List<Discipline> GetDisciplines(
            IQueryable<Course> courses, IQueryable<CourseWithDiscipline> cwd, IQueryable<Discipline> list, int id)
        {
            return cwd
                .Where(cd => cd.CourseId.Equals(id))
                .Join(courses, cd => cd.CourseId, c => c.Id,
                (cd, c) => new { cwd = cd, courses = c })
                .Join(list, cd => cd.cwd.DisciplineId, d => d.Id,
                (cd, d) => new { cwd = cd, list = d }).AsNoTracking()
                .Select(x => x.list).ToList();
        }
    }
}
