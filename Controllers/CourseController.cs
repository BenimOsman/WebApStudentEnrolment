using Microsoft.AspNetCore.Mvc;
using WebApStudentEnrolment.Models;
using WebApStudentEnrolment.Repositories;

namespace WebApStudentEnrolment.Controllers
{
    public class CourseController : Controller                                  // This controller handles all the HTTP requests related to Course operations
    {
        private readonly ICourse _courseRepo;                                   // Dependency injected for accessing course repository interface

        public CourseController(ICourse courseRepo)                             // Constructor injection to get access to the course repository
        {
            _courseRepo = courseRepo;
        }

        // GET: Courses
        // Displays a list of all courses
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepo.GetAllCourses();                    // Fetch all courses
            return View(courses);                                               // Return the course list view
        }

        // GET: Courses/Details/5
        // Displays the details of a specific course by ID
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseRepo.GetCourseById(id);                   // Find course by ID
            if (course == null)
            {
                return NotFound();                                              
            }
            return View(course);                                                // Show course details in view
        }

        // GET: Courses/Create
        // Shows the form to create a new course
        public IActionResult Create()
        {
            return View();                                                      // Return an empty form view for course creation
        }

        // POST: Courses/Create
        // Handles the form submission for adding a new course
        [HttpPost]
        [ValidateAntiForgeryToken]                                              // Protects against CSRF
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Credits")] Course course)
        {
            if (ModelState.IsValid)                                             // Validate model data
            {
                await _courseRepo.AddCourse(course);                            // Add course to the database
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
                return NotFound(); 
            }
            return View(course);                                                // Show form populated with course data
        }

        // POST: Courses/Edit/5
        // Handles form submission for updating course info
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
                await _courseRepo.UpdateCourse(id, course);                     // Update course info in DB
                return RedirectToAction(nameof(Index));                         // Redirect to course list
            }
            return View(course);                                                // Return form with validation errors
        }

        // GET: Courses/Delete/5
        // Shows confirmation page for deleting a course
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepo.GetCourseById(id);                   // Find course by ID
            if (course == null)
            {
                return NotFound(); 
            }
            return View(course);                                                // Show confirmation view
        }

        // POST: Courses/Delete/5
        // Confirms and deletes the course from the DB
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _courseRepo.GetCourseById(id);                   // Double-check existence
            if (course != null)
            {
                await _courseRepo.DeleteCourse(id);                             // Delete course
            }
            return RedirectToAction(nameof(Index));                             // Redirect to course list
        }
    }
}