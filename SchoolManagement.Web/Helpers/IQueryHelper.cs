﻿using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Helpers
{
    public interface IQueryHelper
    {
        List<Discipline> GetDisciplines(
            IQueryable<Course> courses, IQueryable<CourseWithDiscipline> cwd, IQueryable<Discipline> list, int id);
    }
}