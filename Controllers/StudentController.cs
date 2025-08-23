using Microsoft.AspNetCore.Mvc;
using WebApStudentEnrolment.Models;
using WebApStudentEnrolment.Repositories;

namespace WebApStudentEnrolment.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent _studentRepo;                             // Interface to access student data from repository

        public StudentController(IStudent studentRepo)                      // Constructor for accessing the student repository
        {
            _studentRepo = studentRepo;
        }

        // GET: Students
        // Displays a list of all students
        public async Task<IActionResult> Index()
        {
            var students = await _studentRepo.GetAllStudents();             // Fetch all students
            return View(students);                                          // Return view with student list
        }

        // GET: Students/Details/{id}
        // Displays details of a specific student by ID
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentRepo.GetStudentById(id);            // Get student by ID
            if (student == null)
            {
                return NotFound(); 
            }
            return View(student);                                           // Show student details
        }

        // GET: Students/Create
        // Shows form to create a new student
        public IActionResult Create()
        {
            return View();                                                  // Returns an empty form view
        }

        // POST: Students/Create
        // Handles the submission of a new student
        [HttpPost]
        [ValidateAntiForgeryToken]                                          // Prevents CSRF attacks
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Address")] Student student)
        {
            if (ModelState.IsValid)                                         // Check if model passes validation
            {
                await _studentRepo.AddStudent(student);                     // Add new student to database
                return RedirectToAction(nameof(Index));                     // Redirect to list after success
            }
            return View(student);                                           // Return to form if validation fails
        }

        // GET: Students/Edit/5
        // Displays form to edit an existing student
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentRepo.GetStudentById(id);            // Find student by ID
            if (student == null)
            {
                return NotFound(); 
            }
            return View(student);                                           // Return student data to edit view
        }

        // POST: Students/Edit/5
        // Handles submission of edited student data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Address")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();                                          // If ID mismatch, return 404
            }

            if (ModelState.IsValid)                                         // Validate form input
            {
                await _studentRepo.UpdateStudent(id, student);              // Save updates
                return RedirectToAction(nameof(Index));                     // Redirect to list
            }

            return View(student);
        }

        // GET: Students/Delete/5
        // Displays confirmation page before deleting student
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentRepo.GetStudentById(id);            // Find student
            if (student == null)
            {
                return NotFound(); 
            }
            return View(student);                                           // Show confirmation page
        }

        // POST: Students/Delete/5
        // Handles the confirmation and deletes the student
        [HttpPost, ActionName("Delete")] // Matches with the Delete view
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _studentRepo.GetStudentById(id);            // Re-confirm existence
            if (student != null)
            {
                await _studentRepo.DeleteStudent(id);                       // Delete from database
            }

            return RedirectToAction(nameof(Index));                         // Redirect to list
        }
    }
}