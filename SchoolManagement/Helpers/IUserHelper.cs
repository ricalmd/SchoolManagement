using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Data.Entities;

namespace SchoolManagement.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);
    }
}
