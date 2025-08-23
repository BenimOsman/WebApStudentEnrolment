using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApStudentEnrolment.Data;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public class EnrolmentRepo : IEnrolments                                                // This class handles data operations related to Enrolments
    {
        private readonly StudentEnrolmentContext _context;                                  // Private readonly context to access the database

        public EnrolmentRepo(StudentEnrolmentContext context)                               // Constructor to access DB context
        {
            _context = context;
        }

        public int Count => _context.Enrolments.Count();                                    // Counts the total number of enrolments

        public async Task<IActionResult> AddEnrolment(Enrolment enrolment)                  // Adds a new enrolment to the database
        {
            await _context.Enrolments.AddAsync(enrolment);                                  // Adds enrolment to the DbSet
            await _context.SaveChangesAsync();                                              // Commits the change to DB
            return new OkResult();                                                          // Returns 200 OK if successful
        }

        public async Task<IActionResult> GetEnrolmentById(int enrolmentId)                  // Gets a specific enrolment by ID, including related Student and Course
        {
            var enrolment = await _context.Enrolments                                       // Loads the related student and course entity
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == enrolmentId);                             // Finds enrolment by ID

            if (enrolment == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(enrolment);                                           // Returns the enrolment with 200 OK
        }

        
        public async Task<IActionResult> GetAllEnrolments()
        {
            var enrolments = await _context.Enrolments                                      // Returns the full list of enrolments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();

            return new OkObjectResult(enrolments);                                          // Returns 200 OK if successful
        }

        public async Task<IActionResult> UpdateEnrolment(Enrolment enrolment)               // Updates an existing enrolment record
        {
            var existingEnrolment = await _context.Enrolments.FindAsync(enrolment.Id);      // Find existing enrolment
            if (existingEnrolment == null)
            {
                return new NotFoundResult();
            }

            existingEnrolment.StudentId = enrolment.StudentId;                              // Updating the values
            existingEnrolment.CourseId = enrolment.CourseId;
            existingEnrolment.EnrolmentDate = enrolment.EnrolmentDate;

            await _context.SaveChangesAsync();                                              // Save changes to the DB
            return new OkResult();
        }

        public async Task<IActionResult> DeleteEnrolment(int enrolmentId)                   // Deletes an enrolment by its ID
        {
            var enrolment = await _context.Enrolments.FindAsync(enrolmentId);               // Find the enrolment
            if (enrolment == null)
            {
                return new NotFoundResult();
            }

            _context.Enrolments.Remove(enrolment);                                          // Mark enrolment for deletion
            await _context.SaveChangesAsync();                                              // Commit to DB
            return new OkResult();
        }
    }
}

/*
 | Status Code                | Meaning          | When it's used                               |
| --------------------------- | -----------------| -------------------------------------------- |
| `200 OK`                    | Success          | Data fetched, saved, or updated successfully |
| `404 Not Found`             | Resource missing | Item not found in the database               |
| `500 Internal Server Error` | Server crash     | Unexpected error on the server               |
| `400 Bad Request`           | Invalid input    | Client sent wrong or incomplete data         |
 */