using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Data.Repositories;
using SchoolManagement.Web.Helpers;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Controllers
{
    public class TeachersController : Controller
    {
        private readonly IClassRepository _classRepository;
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IClassificationRepository _classificationRepository;
        private readonly IUserHelper _userHelper;

        public TeachersController(
            IClassRepository classRepository, 
            IDisciplineRepository disciplineRepository,
            IClassificationRepository classificationRepository,
            IUserHelper userHelper)
        {
            _classRepository = classRepository;
            _disciplineRepository = disciplineRepository;
            _classificationRepository = classificationRepository;
            _userHelper = userHelper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            return View(_classRepository.GetClassesFromTeacher(user.Id));
        }

        public async Task<IActionResult> DisciplinesIndex(Class classItem)
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            var model = _disciplineRepository.GetDisciplinesFromTeacher(user.Id, classItem.Id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        public IActionResult Details(Discipline discipline, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _classificationRepository.GetClassificationsFromClass(id.Value, discipline.Id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
