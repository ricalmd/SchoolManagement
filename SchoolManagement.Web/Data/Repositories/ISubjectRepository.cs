﻿using System.Linq;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        IQueryable GetAllWithUsers();
    }
}