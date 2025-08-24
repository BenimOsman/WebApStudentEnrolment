using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApStudentEnrolment.Models;
using WebApStudentEnrolment.Repositories;

namespace WebApStudentEnrolment.Controllers
{
    public class CourseController : Controller                                  // Controller to handle HTTP requests for Course operations
    {
        private readonly ICourse _courseRepo;                                   // Repository interface for course data access

        public CourseController(ICourse courseRepo)                             // Constructor injection to access course repository
        {
            _courseRepo = courseRepo;
        }

        // GET: Courses
        // Displays a list of all courses
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepo.GetAllCourses();                    // Fetch all courses
            return View(courses);                                               // Return view with course list
        }

        // GET: Courses/Details/5
        // Displays the details of a specific course by ID
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseRepo.GetCourseById(id);                   // Get course by ID
            if (course == null)
            {
                return NotFound();                                              // Return 404 if course not found
            }
            return View(course);                                                // Return course details view
        }

        // GET: Courses/Create
        // Shows the form to create a new course
        public IActionResult Create()
        {
            return View();                                                      // Return empty form for course creation
        }

        // POST: Courses/Create
        // Handles form submission for adding a new course
        [HttpPost]
        [ValidateAntiForgeryToken]                                              // Protects against CSRF attacks
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Credits")] Course course)
        {
            if (ModelState.IsValid)                                             // Check if input model is valid
            {
                await _courseRepo.AddCourse(course);                            // Add course to database
                return RedirectToAction(nameof(Index));                         // Redirect to course list
            }
            return View(course);                                                // Return form with validation errors
        }

        // GET: Courses/Edit/5
        // Displays the form to edit an existing course
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseRepo.GetCourseById(id);                   // Find course by ID
            if (course == null)
            {
                return NotFound();                                              // Return 404 if course not found
            }
            return View(course);                                                // Return form populated with course data
        }

        // POST: Courses/Edit/5
        // Handles form submission for updating course information
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Credits")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();                                              // Return 404 if ID mismatch
            }

            if (ModelState.IsValid)                                             // Validate model
            {
                try
                {
                    await _courseRepo.UpdateCourse(id, course);                 // Update course in database
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _courseRepo.GetCourseById(id) == null)            // Check if course exists after concurrency exception
                    {
                        return NotFound();
                    }
                    throw;                                                      // Rethrow exception if still failing
                }
                return RedirectToAction(nameof(Index));                         // Redirect to course list after success
            }
            return View(course);                                                // Return form with validation errors
        }

        // GET: Courses/Delete/5
        // Shows confirmation page before deleting a course
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepo.GetCourseById(id);                   // Find course by ID
            if (course == null)
            {
                return NotFound();                                              // Return 404 if not found
            }
            return View(course);                                                // Show confirmation page
        }

        // POST: Courses/Delete/5
        // Handles confirmation and deletes the course
        [HttpPost, ActionName("Delete")]                                         // Match form action in Delete view
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _courseRepo.GetCourseById(id);                   // Double-check course exists
            if (course != null)
            {
                await _courseRepo.DeleteCourse(id);                             // Delete course from database
            }
            return RedirectToAction(nameof(Index));                             // Redirect to course list
        }
    }
}