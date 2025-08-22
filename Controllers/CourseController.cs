using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApStudentEnrolment.Models;
using WebApStudentEnrolment.Repositories;

namespace WebApStudentEnrolment.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourse _courseRepo;
        public CourseController(ICourse courseRepo)
        {
            _courseRepo = courseRepo;
        }
        // GET: Courses
        public async Task <IActionResult> Index()
        {
           var courses=  await _courseRepo.GetAllCourses();

            return View(courses);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseRepo.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // GET: Courses/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Credits")] Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseRepo.AddCourse(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseRepo.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Credits")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _courseRepo.UpdateCourse(id, course);
                }
                catch (DbUpdateConcurrencyException )
                {
                    if (await _courseRepo.GetCourseById(id) == null)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepo.GetCourseById(id);
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
            var course = await _courseRepo.GetCourseById(id);
            if (course != null)
            {
                await _courseRepo.DeleteCourse(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
