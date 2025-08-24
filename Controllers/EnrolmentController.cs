using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApStudentEnrolment.Data;
using WebApStudentEnrolment.Models;
using WebApStudentEnrolment.Repositories;

namespace WebApStudentEnrolment.Controllers
{
    public class EnrolmentController : Controller                                // This controller handles enrolment operations (Create, Read, Update, Delete)
    {
        private readonly IEnrolments _enrollmentRepo;                            // Repository for enrolment data
        private readonly StudentEnrolmentContext _context;                       // Context for accessing students and courses to populate dropdowns

        // Constructor with dependency injection for the repository and context
        public EnrolmentController(IEnrolments enrollmentRepo, StudentEnrolmentContext context)
        {
            _enrollmentRepo = enrollmentRepo;
            _context = context;
        }

        // GET: Enrollments
        // Displays a list of all enrolments
        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentRepo.GetAllEnrolments();          // Fetch all enrolments
            return View(enrollments);                                            // Return view with enrolment list
        }

        // GET: Enrollments/Details/5
        // Displays details of a specific enrolment
        public async Task<IActionResult> Details(int id)
        {
            var enrollment = await _enrollmentRepo.GetEnrolmentById(id);        // Get enrolment by ID
            if (enrollment == null)
            {
                return NotFound();                                              // Return 404 if not found
            }
            return View(enrollment);                                            // Return details view
        }

        // GET: Enrollments/Create
        // Shows the form to create a new enrolment
        public IActionResult Create()
        {
            // Populate dropdowns for selecting Student and Course
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            return View();                                                      // Show empty form
        }

        // POST: Enrollments/Create
        // Handles form submission for new enrolment
        [HttpPost]
        [ValidateAntiForgeryToken]                                              // Protect against CSRF
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId,EnrolmentDate")] Enrolment enrollment)
        {
            if (ModelState.IsValid)                                             // Validate form input
            {
                await _enrollmentRepo.AddEnrolment(enrollment);                 // Add enrolment to database
                return RedirectToAction(nameof(Index));                         // Redirect to list after success
            }

            // If validation fails, repopulate dropdowns
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", enrollment.StudentId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseId);
            return View(enrollment);                                            // Return form with validation errors
        }

        // GET: Enrollments/Edit/5
        // Shows a form to edit an existing enrolment
        public async Task<IActionResult> Edit(int id)
        {
            var enrollment = await _enrollmentRepo.GetEnrolmentById(id);        // Get enrolment by ID
            if (enrollment == null)
            {
                return NotFound();                                              // Return 404 if not found
            }

            // Populate dropdowns with selected values
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", enrollment.StudentId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", enrollment.CourseId);
            return View(enrollment);                                            // Show form with current data
        }

        // POST: Enrollments/Edit/5
        // Handles form submission for updating enrolment info
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,CourseId,EnrolmentDate")] Enrolment enrolment)
        {
            if (id != enrolment.Id)
            {
                return NotFound();                                              // Return 404 if ID mismatch
            }

            if (ModelState.IsValid)                                             // Validate form input
            {
                try
                {
                    await _enrollmentRepo.UpdateEnrolment(id, enrolment);       // Update enrolment in database
                }
                catch (DbUpdateConcurrencyException)                             // Handle concurrency conflicts
                {
                    if (await _enrollmentRepo.GetEnrolmentById(id) == null)     // Check if enrolment still exists
                    {
                        return NotFound();                                      // Return 404 if deleted in meantime
                    }
                    throw;                                                      // Rethrow if other error
                }
                return RedirectToAction(nameof(Index));                         // Redirect to list
            }

            // If validation fails, repopulate dropdowns
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", enrolment.StudentId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", enrolment.CourseId);
            return View(enrolment);                                              // Return form with validation errors
        }

        // GET: Enrollments/Delete/5
        // Shows confirmation page before deleting enrolment
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _enrollmentRepo.GetEnrolmentById(id);        // Get enrolment by ID
            if (enrollment == null)
            {
                return NotFound();                                              // Return 404 if not found
            }
            return View(enrollment);                                            // Show confirmation page
        }

        // POST: Enrollments/Delete/5
        // Handles the actual deletion after confirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _enrollmentRepo.DeleteEnrolment(id);                           // Delete enrolment from database
            return RedirectToAction(nameof(Index));                               // Redirect to list
        }
    }
}
