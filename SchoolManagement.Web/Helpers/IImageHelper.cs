using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }
}
