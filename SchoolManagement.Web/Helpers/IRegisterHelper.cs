using System.Threading.Tasks;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Helpers
{
    public interface IRegisterHelper
    {
        Task AddStudentAsync(User user, int id);

        Task AddTeacherAsync(User user, int id);
    }
}