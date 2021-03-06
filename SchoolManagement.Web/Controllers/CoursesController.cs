﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Data.Repositories;
using SchoolManagement.Web.Helpers;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseWithDisciplineRepository _courseWithDisciplineRepository;
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IClassificationRepository _classificationRepository;
        private readonly IUserHelper _userHelper;
        private readonly IClassRepository _classRepository;

        public CoursesController(
            ICourseRepository courseRepository,
            ICourseWithDisciplineRepository courseWithDisciplinesRepository,
            IDisciplineRepository disciplineRepository,
            IClassificationRepository classificationRepository,
            IUserHelper userHelper,
            IClassRepository classRepository)
        {
            _courseRepository = courseRepository;
            _courseWithDisciplineRepository = courseWithDisciplinesRepository;
            _disciplineRepository = disciplineRepository;
            _classificationRepository = classificationRepository;
            _userHelper = userHelper;
            _classRepository = classRepository;
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

            var result = _disciplineRepository.GetDisciplines(id.Value);

            var model = _courseRepository.CourseAndDisciplines(course, result);

            if (course == null || result == null || model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = "Administrativo")]
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

        [Authorize(Roles = "Administrativo")]
        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRepository.GetByIdAsync(id.Value);
            var model = _courseRepository.ToAddDisciplinesViewModel(course);
            
            if (course == null || model == null)
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

                    var cwdAll = _courseWithDisciplineRepository.GetCwd(course.Id, model.DisciplineId);
                    var cwd = _courseWithDisciplineRepository.ToAddCourseWithDisciplines(course, course.User, model.DisciplineId);
                    var classifications = _classificationRepository.GetClassificationForStudents(course.Id);

                    if (cwd.DisciplineId != 0 && !cwdAll.Any())
                    {
                        await _courseWithDisciplineRepository.CreateAsync(cwd);

                        if (classifications.Any())
                        {
                            int elemen = 0;

                            foreach (var item in classifications)
                            {
                                if (item.StudentId != elemen)
                                {
                                    var cl = new Classification
                                    {
                                        DisciplineId = model.DisciplineId,
                                        JustifiedAbsence = 0,
                                        UnjustifiedAbsence = 0,
                                        Score = 0,
                                        StudentId = item.StudentId
                                    };
                                    elemen = item.StudentId;
                                    await _classificationRepository.CreateAsync(cl);
                                }
                            }
                        }
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

        [Authorize(Roles = "Administrativo")]
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
            var cwd = _courseWithDisciplineRepository.GetByCourseId(id);
            var classItem = _classRepository.GetClassesFromCourse(id);

            if (cwd.Count() == 0 && classItem.Count() == 0)
            {
                var course = await _courseRepository.GetByIdAsync(id);
                await _courseRepository.DeleteAsync(course);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("ErrorDeleteCourse");
            }
        }

        [Authorize(Roles = "Administrativo")]
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCwd(int? id, int? disciplineId)
        {
            if (id == null || disciplineId == null)
            {
                return NotFound();
            }

            var cwd = _courseWithDisciplineRepository.GetCwd(id.Value, disciplineId.Value).FirstOrDefault();
            if (cwd == null)
            {
                return NotFound();
            }

            await _courseWithDisciplineRepository.DeleteCwdAsync(cwd);
            var elements = _classificationRepository.GetClassificationFromCourse(id.Value, disciplineId.Value);

            if (elements.Any())
            {
                foreach(var item in elements)
                {
                    await _classificationRepository.DeleteAsync(item);
                }
            }

            return this.RedirectToAction($"Details/{id}");
        }

        public IActionResult ErrorDeleteCourse()
        {
            return View();
        }

        public async Task<IActionResult> GeneratePDFCourse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _courseRepository.GetByIdAsync(id.Value);

            var result = _disciplineRepository.GetDisciplines(id.Value);

            var model = _courseRepository.CourseAndDisciplines(course, result);

            if (course == null || result == null || model == null)
            {
                return NotFound();
            }

            var pdf = new ViewAsPdf(model);

            return pdf;
        }
    }
}
