using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApStudentEnrolment.Models;
using WebApStudentEnrolment.Repositories;

namespace WebApStudentEnrolment.Controllers
{
    public class EnrolmentController : Controller                               // This controller handles enrolment operations (Create, Read, Update, Delete)
    {
        private readonly IEnrolments _enrolmentRepo;                            // Declare private fields to hold the injected repositories
        private readonly IStudent _studentRepo;
        private readonly ICourse _courseRepo;

        // Constructor with dependency injection for all 3 repositories
        public EnrolmentController(IEnrolments enrolmentRepo, IStudent studentRepo, ICourse courseRepo)
        {
            _enrolmentRepo = enrolmentRepo;
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
        }

        // GET: Enrolments
        // Displays the list of all enrolments
        public async Task<IActionResult> Index()
        {
            var result = await _enrolmentRepo.GetAllEnrolments();               // Fetch all enrolments from the repository (including related Course and Student)

            if (result is OkObjectResult okResult && okResult.Value is IEnumerable<Enrolment> enrolments)
            {
                return View(enrolments);                                        // Render the enrolment list view
            }
            return View(Enumerable.Empty<Enrolment>());
        }

        // GET: Enrolments/Details/5
        // Displays detailed info about a specific enrolment
        public async Task<IActionResult> Details(int id)
        {
            var result = await _enrolmentRepo.GetEnrolmentById(id);

            if (result is OkObjectResult okResult && okResult.Value is Enrolment enrolment)
            {
                return View(enrolment);                                         // Show enrolment details
            }

            return NotFound(); 
        }

        // GET: Enrolments/Create
        // Renders the form for creating a new enrolment
        public async Task<IActionResult> Create()
        {
            // Populate dropdowns for selecting Student and Course
            ViewData["StudentId"] = new SelectList(await _studentRepo.GetAllStudents(), "Id", "Name");
            ViewData["CourseId"] = new SelectList(await _courseRepo.GetAllCourses(), "Id", "Description");

            return View(); // Show empty form
        }

        // POST: Enrolments/Create
        // Handles form submission for new enrolment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId,EnrolmentDate")] Enrolment enrolment)
        {
            if (ModelState.IsValid)
            {
                await _enrolmentRepo.AddEnrolment(enrolment);                   // Save to DB
                return RedirectToAction(nameof(Index));                         // Redirect to enrolment list
            }

            // If validation fails, re-populate dropdowns and return form with errors
            ViewData["StudentId"] = new SelectList(await _studentRepo.GetAllStudents(), "Id", "Name", enrolment.StudentId);
            ViewData["CourseId"] = new SelectList(await _courseRepo.GetAllCourses(), "Id", "Description", enrolment.CourseId);
            return View(enrolment);
        }

        // GET: Enrolments/Edit/5
        // Shows a form to edit an existing enrolment
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _enrolmentRepo.GetEnrolmentById(id);

            if (result is OkObjectResult okResult && okResult.Value is Enrolment enrolment)
            {
                // Populate dropdowns with selected values
                ViewData["StudentId"] = new SelectList(await _studentRepo.GetAllStudents(), "Id", "Name", enrolment.StudentId);
                ViewData["CourseId"] = new SelectList(await _courseRepo.GetAllCourses(), "Id", "Description", enrolment.CourseId);
                return View(enrolment);                                         // Show form with current data
            }

            return NotFound();
        }

        // GET: Enrolments/Delete/5
        // Shows confirmation page before deletion
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _enrolmentRepo.GetEnrolmentById(id);

            if (result is OkObjectResult okResult && okResult.Value is Enrolment enrolment)
            {
                return View(enrolment);                                         // Show confirmation view
            }

            return NotFound();                                                  // Return 404 if not found
        }

        // POST: Enrolments/Delete/5
        // Handles actual deletion of the enrolment after confirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _enrolmentRepo.DeleteEnrolment(id);                           // Delete from DB
            return RedirectToAction(nameof(Index));                             // Redirect to list
        }
    }
}