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
        private readonly ICourseWithSubjectsRepository _courseWithSubjectsRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IUserHelper _userHelper;
        private readonly ICourseAndSubjectsHelper _courseAndSubjectsHelper;

        public CoursesController(
            ICourseRepository courseRepository,
            ICourseWithSubjectsRepository courseWithSubjectsRepository,
            ISubjectRepository subjectRepository,
            IUserHelper userHelper,
            ICourseAndSubjectsHelper courseAndSubjectsHelper)
        {
            _courseRepository = courseRepository;
            _courseWithSubjectsRepository = courseWithSubjectsRepository;
            _subjectRepository = subjectRepository;
            _userHelper = userHelper;
            _courseAndSubjectsHelper = courseAndSubjectsHelper;
        }

        // GET: Courses
        public IActionResult Index()
        {
            return View(_courseRepository.GetAll().OrderBy(c => c.Name));
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id, Subject subject)
        {
            if(id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetByIdAsync(id.Value);
            var list = _courseWithSubjectsRepository.GetCourseWithSubjects(id.Value);
            
            subject.SubjectsAndCourses = list.ToList();

            var cws = _subjectRepository.GetSubjectsWithCourse(list);

            if (course == null)
            {
                return NotFound();
            }

            var model = _courseAndSubjectsHelper.ToCourseSubjectViewModel(course, cws);
            
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
            var model = _courseRepository.ToAddSubjectsViewModel(course);

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
        public async Task<IActionResult> Edit(Course course, AddSubjectsViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    course.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _courseRepository.UpdateAsync(course);
                    
                    var cws = _courseWithSubjectsRepository.ToAddCourseWithSubjects(course.Id, model);
                    await _courseWithSubjectsRepository.CreateAsync(cws);
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
            await _courseRepository.DeleteAsync(course);
            return RedirectToAction(nameof(Index));
        }
    }
}
