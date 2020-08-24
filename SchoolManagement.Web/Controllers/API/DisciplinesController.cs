using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Web.Data.Repositories;

namespace SchoolManagement.Web.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class DisciplinesController : Controller
    {
        private readonly IDisciplineRepository _disciplineRepository;

        public DisciplinesController(IDisciplineRepository disciplineRepository)
        {
            _disciplineRepository = disciplineRepository;
        }

        [HttpGet]
        public IActionResult GetDisciplines()
        {
            return Ok(_disciplineRepository.GetAllWithUsers());
        }
    }
}
