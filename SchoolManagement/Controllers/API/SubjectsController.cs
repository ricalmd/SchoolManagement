using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Data;

namespace SchoolManagement.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectsController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        [HttpGet]
        public IActionResult GetSubjects()
        {
            return Ok(_subjectRepository.GetAllWithUsers());
        }
    }
}
