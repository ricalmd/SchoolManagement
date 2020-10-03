using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Data.Repositories;
using SchoolManagement.Web.Helpers;

namespace SchoolManagement.Web.Controllers
{
    public class TeachersController : Controller
    {
        private readonly IClassRepository _classRepository;
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IClassificationRepository _classificationRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserHelper _userHelper;

        public TeachersController(
            IClassRepository classRepository, 
            IDisciplineRepository disciplineRepository,
            IClassificationRepository classificationRepository,
            ITeacherRepository teacherRepository,
            IUserHelper userHelper)
        {
            _classRepository = classRepository;
            _disciplineRepository = disciplineRepository;
            _classificationRepository = classificationRepository;
            _teacherRepository = teacherRepository;
            _userHelper = userHelper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            return View(_classRepository.GetClassesFromTeacher(user.Id));
        }

        public async Task<IActionResult> DisciplinesIndex(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
           
            var model = _disciplineRepository.GetDisciplinesFromTeacher(user.Id, id.Value);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        public IActionResult Details(int? id, int? classId)
        {
            if(id == null || classId == null)
            {
                return NotFound();
            }

            var model = _classificationRepository
                .GetClassificationsFromClass(classId.Value, id.Value);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(
            int? clId, int score, int jAbsence, int uAbsence, int studentId, int disciplineId)
        {
            var classification = new Classification
            {
                Id = clId.Value,
                DisciplineId = disciplineId,
                StudentId = studentId,
                JustifiedAbsence = jAbsence,
                Score = score,
                UnjustifiedAbsence = uAbsence
            };

            try
            {
                await _classificationRepository.UpdateAsync(classification);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _classificationRepository.ExistAsync(classification.Id))
                {
                   return NotFound();
                }
                else
                {
                throw;
                }
            }
            return this.RedirectToAction(nameof(Index));
        }

        public IActionResult TeachersIndex()
        {
            return View(_teacherRepository.GetUsersFromTeachers());
        }

        [Authorize(Roles = "Administrativo")]
        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? teacherId)
        {
            if (teacherId == null)
            {
                return NotFound();
            }

            var teacher = await _teacherRepository.GetByIdAsync(teacherId.Value);

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int teacherId)
        {
            var teacher = await _teacherRepository.GetByIdAsync(teacherId);

            await _teacherRepository.DeleteAsync(teacher);
            return RedirectToAction("TeachersIndex");
        }
    }
}
