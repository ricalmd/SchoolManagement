using System.Collections.Generic;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Data.Repositories
{
    public interface IClassificationRepository : IGenericRepository<Classification>
    {
        List<UsersAndClassificationViewModel> GetClassification(string id, int classId);

        List<UsersAndClassificationViewModel> GetClassificationsFromClass(int classId, int disciplineId);

        List<Classification> GetClassificationFromCourse(int courseId, int disciplineId);

        List<Classification> GetClassificationForStudents(int courseId);
    }
}
