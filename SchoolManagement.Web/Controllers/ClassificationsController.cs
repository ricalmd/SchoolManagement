using System.Threading.Tasks;
using System.Linq;
using SchoolManagement.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Web.Helpers;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Authorization;

namespace SchoolManagement.Web.Controllers
{
    [Authorize(Roles = "Aluno")]
    public class ClassificationsController : Controller
    {
        private readonly IClassificationRepository _classificationRepository;
        private readonly IClassRepository _classRepository;
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IUserHelper _userHelper;
        private readonly ICourseRepository _courseRepository;

        public ClassificationsController(
            IClassificationRepository classificationRepository,
            IClassRepository classRepository,
            IDisciplineRepository disciplineRepository,
            IUserHelper userHelper,
            ICourseRepository courseRepository)
        {
            _classificationRepository = classificationRepository;
            _classRepository = classRepository;
            _disciplineRepository = disciplineRepository;
            _userHelper = userHelper;
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            return View(_classRepository.GetClassesFromUser(user.Id).OrderBy(c => c.NameClass));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            var item = _classificationRepository.GetClassification(user.Id, id.Value);

            if (item == null)
            {
                return NotFound();
            }

            var course = _courseRepository.GetCourseByStudent(item.First().StudentId);

            ViewBag.Message = course;
            return View(item);
        }

        public IActionResult GeneratePDF(string userId, int classId)
        {
            if(userId == null)
            {
                return NotFound();
            }
            var cl = _classificationRepository.GetClassification(userId, classId).ToList();

            var pdf = new ViewAsPdf(cl);

            return pdf;
        }
    }
}
