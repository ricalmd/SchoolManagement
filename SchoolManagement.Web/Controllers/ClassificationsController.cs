using System.Threading.Tasks;
using System.Linq;
using SchoolManagement.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Web.Helpers;

namespace SchoolManagement.Web.Controllers
{
    public class ClassificationsController : Controller
    {
        private readonly IClassificationRepository _classificationRepository;
        private readonly IClassRepository _classRepository;
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IUserHelper _userHelper;

        public ClassificationsController(
            IClassificationRepository classificationRepository,
            IClassRepository classRepository,
            IDisciplineRepository disciplineRepository,
            IUserHelper userHelper)
        {
            _classificationRepository = classificationRepository;
            _classRepository = classRepository;
            _disciplineRepository = disciplineRepository;
            _userHelper = userHelper;
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

            return View(item);
        }
    }
}
