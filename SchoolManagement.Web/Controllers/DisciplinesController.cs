using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Data.Repositories;
using SchoolManagement.Web.Helpers;

namespace SchoolManagement.Web.Controllers
{
    [Authorize(Roles = "Administrativo")]
    public class DisciplinesController : Controller
    {
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IUserHelper _userHelper;

        public DisciplinesController(
            IDisciplineRepository disciplineRepository,  
            IUserHelper userHelper)
        {
            _disciplineRepository = disciplineRepository;
            _userHelper = userHelper;
        }

        // GET: Subjects
        public IActionResult Index()
        {
            return View(_disciplineRepository.GetAll().OrderBy(s => s.Name));
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discipline = await _disciplineRepository.GetByIdAsync(id.Value);
            if (discipline == null)
            {
                return NotFound();
            }

            return View(discipline);
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Discipline discipline)
        {
            if (ModelState.IsValid)
            {
                discipline.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _disciplineRepository.CreateAsync(discipline);
                return RedirectToAction(nameof(Index));
            }
            return View(discipline);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discipline = await _disciplineRepository.GetByIdAsync(id.Value);
            if (discipline == null)
            {
                return NotFound();
            }
            return View(discipline);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Discipline discipline)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    discipline.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _disciplineRepository.UpdateAsync(discipline);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _disciplineRepository.ExistAsync(discipline.Id))
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
            return View(discipline);
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discipline = await _disciplineRepository.GetByIdAsync(id.Value);
            if (discipline == null)
            {
                return NotFound();
            }

            return View(discipline);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discipline = await _disciplineRepository.GetByIdAsync(id);
            await _disciplineRepository.DeleteAsync(discipline);
            return RedirectToAction(nameof(Index));
        }
    }
}
