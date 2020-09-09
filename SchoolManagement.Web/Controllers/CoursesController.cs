using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Data.Repositories;
using SchoolManagement.Web.Helpers;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseWithDisciplineRepository _courseWithDisciplineRepository;
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IUserHelper _userHelper;
        private readonly IQueryHelper _queryHelper;

        public CoursesController(
            ICourseRepository courseRepository,
            ICourseWithDisciplineRepository courseWithDisciplinesRepository,
            IDisciplineRepository disciplineRepository,
            IUserHelper userHelper,
            IQueryHelper queryHelper)
        {
            _courseRepository = courseRepository;
            _courseWithDisciplineRepository = courseWithDisciplinesRepository;
            _disciplineRepository = disciplineRepository;
            _userHelper = userHelper;
            _queryHelper = queryHelper;
        }

        // GET: Courses
        public IActionResult Index()
        {
            return View(_courseRepository.GetAll().OrderBy(c => c.Name));
        }
        
        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetByIdAsync(id.Value);

            var courses = _courseRepository.GetAll();
            var cwd = _courseWithDisciplineRepository.GetAll();
            var list = _disciplineRepository.GetAll();

            var result = _queryHelper.GetDisciplines(courses, cwd, list, id.Value);

            var model = _courseRepository.CourseAndDisciplines(course, result);

            if (course == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                course.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _courseRepository.CreateAsync(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetByIdAsync(id.Value);
            var model = _courseRepository.ToAddDisciplinesViewModel(course);
            
            if (course == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Course course, AddDisciplinesViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    course.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _courseRepository.UpdateAsync(course);
                    
                    var cwd = _courseWithDisciplineRepository.ToAddCourseWithDisciplines(course.Id, course.User, model);

                    if (cwd.DisciplineId != 0)
                    {
                        await _courseWithDisciplineRepository.CreateAsync(cwd);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _courseRepository.ExistAsync(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetByIdAsync(id.Value);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            int cwd = _courseWithDisciplineRepository.GetByCourseId(id).Count();

            while(cwd > 0)
            {
                var cwdList = _courseWithDisciplineRepository.GetByCourseId(id).FirstOrDefault();
                await _courseWithDisciplineRepository.DeleteAsync(cwdList);
                cwd--;
            }

            await _courseRepository.DeleteAsync(course);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCwd(int? id, int? disciplineId)
        {
            if (id == null || disciplineId == null)
            {
                return NotFound();
            }

            var cwd = _courseWithDisciplineRepository.GetCwdAsync(id.Value, disciplineId.Value).FirstOrDefault();
            if (cwd == null)
            {
                return NotFound();
            }

            await _courseWithDisciplineRepository.DeleteCwdAsync(cwd);
            return this.RedirectToAction($"Details/{id}");
        }
    }
}
