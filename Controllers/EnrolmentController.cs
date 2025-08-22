using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApStudentEnrolment.Models;
using WebApStudentEnrolment.Repositories;

namespace WebApStudentEnrolment.Controllers
{
    public class EnrolmentController : Controller
    {
        private readonly IEnrolments _enrolmentRepo;
        private readonly IStudent _studentRepo;
        private readonly ICourse _courseRepo;

        public EnrolmentController(IEnrolments enrolmentRepo, IStudent studentRepo, ICourse courseRepo)
        {
            _enrolmentRepo = enrolmentRepo;
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
        }

        // GET: Enrolments
        public async Task<IActionResult> Index()
        {
            // Use repository method to get enrolments with related data
            var result = await _enrolmentRepo.GetAllEnrolments();

            // Assuming GetAllEnrolments returns OkObjectResult with the list,
            // extract the list and pass it to the view:
            if (result is OkObjectResult okResult && okResult.Value is IEnumerable<Enrolment> enrolments)
            {
                return View(enrolments);
            }

            // In case of error, return empty view or error view
            return View(Enumerable.Empty<Enrolment>());
        }

        // GET: Enrolments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _enrolmentRepo.GetEnrolmentById(id);

            if (result is OkObjectResult okResult && okResult.Value is Enrolment enrolment)
            {
                return View(enrolment);
            }

            return NotFound();
        }

        // GET: Enrolments/Create
        public async Task<IActionResult> Create()
        {
            ViewData["StudentId"] = new SelectList(await _studentRepo.GetAllStudents(), "Id", "Name");
            ViewData["CourseId"] = new SelectList(await _courseRepo.GetAllCourses(), "Id", "Description");
            return View();
        }

        // POST: Enrolments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId,EnrolmentDate")] Enrolment enrolment)
        {
            if (ModelState.IsValid)
            {
                await _enrolmentRepo.AddEnrolment(enrolment);
                return RedirectToAction(nameof(Index));
            }

            ViewData["StudentId"] = new SelectList(await _studentRepo.GetAllStudents(), "Id", "Name", enrolment.StudentId);
            ViewData["CourseId"] = new SelectList(await _courseRepo.GetAllCourses(), "Id", "Description", enrolment.CourseId);
            return View(enrolment);
        }

        // GET: Enrolments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _enrolmentRepo.GetEnrolmentById(id);

            if (result is OkObjectResult okResult && okResult.Value is Enrolment enrolment)
            {
                ViewData["StudentId"] = new SelectList(await _studentRepo.GetAllStudents(), "Id", "Name", enrolment.StudentId);
                ViewData["CourseId"] = new SelectList(await _courseRepo.GetAllCourses(), "Id", "Description", enrolment.CourseId);
                return View(enrolment);
            }

            return NotFound();
        }

        


        // GET: Enrolments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _enrolmentRepo.GetEnrolmentById(id);

            if (result is OkObjectResult okResult && okResult.Value is Enrolment enrolment)
            {
                return View(enrolment);
            }

            return NotFound();
        }

        // POST: Enrolments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _enrolmentRepo.DeleteEnrolment(id);
            return RedirectToAction(nameof(Index));
        }
    }
}