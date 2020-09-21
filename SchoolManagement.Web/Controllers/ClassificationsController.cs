using System.Threading.Tasks;
using SchoolManagement.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Web.Helpers;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Controllers
{
    public class ClassificationsController : Controller
    {
        private readonly IClassificationRepository _classificationRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IClassRepository _classRepository;
        private readonly ICourseWithDisciplineRepository _courseWithDisciplineRepository;
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IUserHelper _userHelper;

        public ClassificationsController(
            IClassificationRepository classificationRepository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository,
            IClassRepository classRepository,
            ICourseWithDisciplineRepository courseWithDisciplineRepository,
            IDisciplineRepository disciplineRepository,
            IUserHelper userHelper)
        {
            _classificationRepository = classificationRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _classRepository = classRepository;
            _courseWithDisciplineRepository = courseWithDisciplineRepository;
            _disciplineRepository = disciplineRepository;
            _userHelper = userHelper;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var users = _userHelper.GetAllUsers();
            var students = _studentRepository.GetAll();
            var courses = _courseRepository.GetAll();
            var classes = _classRepository.GetAll();
            var cwd = _courseWithDisciplineRepository.GetAll();
            var disciplines = _disciplineRepository.GetAll();
            var classifications = _classificationRepository.GetAll();

            var item = _classificationRepository.GetClassification(
                students, cwd, disciplines, users, classes, courses, classifications, user.Id);
            
            if (item == null)
            {
                return NotFound();
            }

            var model = new ClassificationViewModel
            {
                User = user,
                Classifications = item
            };

            return View(model);
        }
    }
}
