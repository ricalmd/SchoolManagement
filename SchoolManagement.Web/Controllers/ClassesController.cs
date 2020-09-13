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
    [Authorize(Roles = "Administrativo")]
    public class ClassesController : Controller
    {
        private readonly IClassRepository _classRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserHelper _userHelper;

        public ClassesController(
            IClassRepository classRepository,
            ICourseRepository courseRepository,
            IUserHelper userHelper)
        {
            _classRepository = classRepository;
            _courseRepository = courseRepository;
            _userHelper = userHelper;
        }

        // GET: Classes
        public IActionResult Index()
        {
            return View(_classRepository.GetAll().OrderBy(c => c.NameClass));
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemClass = await _classRepository.GetByIdAsync(id.Value);
            itemClass.Course = _courseRepository.GetAllWithCourse(itemClass.CourseId);

            if (itemClass == null)
            {
                return NotFound();
            }

            return View(itemClass);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            var model = new ClassAndCourseViewModel
            {
                Courses = _classRepository.GetComboCourses()
            };

            return View(model);
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassAndCourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                var course = await _courseRepository.GetCourseAsync(model.CourseId);

                var itemClass = new Class 
                {
                    Id = model.Id,
                    NameClass = model.NameClass,
                    BeginSchedule = model.BeginSchedule,
                    EndSchedule = model.EndSchedule,
                    Course = course,
                    CourseId = model.CourseId,
                    User = model.User
                };

                await _classRepository.CreateAsync(itemClass);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemClass = await _classRepository.GetByIdAsync(id.Value);
            if (itemClass == null)
            {
                return NotFound();
            }
            return View(itemClass);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Class itemClass)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    itemClass.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _classRepository.UpdateAsync(itemClass);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _classRepository.ExistAsync(itemClass.Id))
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
            return View(itemClass);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemClass = await _classRepository.GetByIdAsync(id.Value);
            if (itemClass == null)
            {
                return NotFound();
            }

            return View(itemClass);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemClass = await _classRepository.GetByIdAsync(id);
            await _classRepository.DeleteAsync(itemClass);
            return RedirectToAction(nameof(Index));
        }
    }
}
