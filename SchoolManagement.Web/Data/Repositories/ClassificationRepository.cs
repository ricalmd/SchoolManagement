using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public class ClassificationRepository : GenericRepository<Classification>, IClassificationRepository
    {
        private readonly DataContext _context;

        public ClassificationRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public List<UsersAndClassificationViewModel> GetClassification(string id, int classId)
        {
            return (from u in _context.Users
                    join s in _context.Students on u.Id equals s.User.Id
                    join c in _context.Classes on s.Class.Id equals c.Id
                    join co in _context.Courses on c.CourseId equals co.Id
                    join cd in _context.CourseWithDisciplines on co.Id equals cd.Course.Id
                    join d in _context.Disciplines on cd.DisciplineId equals d.Id
                    join cl in _context.Classifications on new { a = d.Id, b = s.Id } equals new { a = cl.DisciplineId, b = cl.StudentId }
                    where u.Id == id && c.Id == classId
                    select new UsersAndClassificationViewModel 
                    {
                        Score = cl.Score,
                        JustifiedAbsence = cl.JustifiedAbsence,
                        User = u,
                        UnjustifiedAbsence = cl.UnjustifiedAbsence,
                        Discipline = d,
                        StudentId = cl.StudentId
                    }).ToList(); 
        }

        public List<UsersAndClassificationViewModel> GetClassificationsFromClass(int classId, int disciplineId)
        {
            return (from u in _context.Users
                    join s in _context.Students on u.Id equals s.User.Id
                    join c in _context.Classes on s.ClassId equals c.Id
                    join cl in _context.Classifications on s.Id equals cl.StudentId
                    join d in _context.Disciplines on cl.DisciplineId equals d.Id
                    where c.Id == classId && d.Id == disciplineId
                    select new UsersAndClassificationViewModel 
                    { 
                        Score = cl.Score,
                        JustifiedAbsence = cl.JustifiedAbsence, 
                        User = u,
                        UnjustifiedAbsence = cl.UnjustifiedAbsence,
                        Id = cl.Id,
                        DisciplineId = d.Id,
                        StudentId = s.Id,
                        Class = c
                    }).ToList();
        }

        public List<Classification> GetClassificationFromCourse(int courseId, int disciplineId)
        {
            return (from co in _context.Courses
                    join c in _context.Classes on co.Id equals c.CourseId
                    join s in _context.Students on c.Id equals s.ClassId
                    join cl in _context.Classifications on s.Id equals cl.StudentId
                    join d in _context.Disciplines on cl.DisciplineId equals d.Id
                    where d.Id == disciplineId && co.Id == courseId
                    select new Classification
                    {
                        Score = cl.Score,
                        JustifiedAbsence = cl.JustifiedAbsence,
                        UnjustifiedAbsence = cl.UnjustifiedAbsence,
                        Id = cl.Id,
                        DisciplineId = cl.DisciplineId,
                        StudentId = cl.StudentId
                    }).ToList();
        }

        public List<Classification> GetClassificationForStudents(int courseId)
        {
            return (from co in _context.Courses
                    join c in _context.Classes on co.Id equals c.CourseId
                    join s in _context.Students on c.Id equals s.ClassId
                    join cl in _context.Classifications on s.Id equals cl.StudentId
                    where co.Id == courseId
                    select new Classification
                    {
                        StudentId = cl.StudentId
                    }).ToList();
        }
    }
}