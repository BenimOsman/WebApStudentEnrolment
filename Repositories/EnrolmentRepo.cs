using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApStudentEnrolment.Data;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public class EnrolmentRepo : IEnrolments
    {
        public EnrolmentRepo() { }
        private readonly StudentEnrolmentContext _context;
        public EnrolmentRepo(StudentEnrolmentContext context)
        {
            _context = context;
        }

        public int Count { get; private set; }

        public async Task<IActionResult> AddEnrolment(Enrolment enrolment)
        {
            // Implementation for adding an enrolment
            await _context.Enrolments.AddAsync(enrolment);
            await _context.SaveChangesAsync();
            return new OkResult(); // Return an appropriate result, e.g., Ok or Created
        }

        public async Task<IActionResult> GetEnrolmentById(int enrolmentId)
        {
            // Implementation for retrieving an enrolment by ID
            var enrolment = await _context.Enrolments.FindAsync(enrolmentId);
            if (enrolment == null)
            {
                return new NotFoundResult(); // Return 404 if not found
            }
            return new OkObjectResult(enrolment); // Return the enrolment object
        }

        public async Task<IActionResult> GetAllEnrolments()
        {
            // Implementation for retrieving all enrolments
            var enrolments = await _context.Enrolments.ToListAsync();
            return new OkObjectResult(enrolments); // Return the list of enrolments
        }

        public async Task<IActionResult> UpdateEnrolment(int enrolmentId)
        {
            // Implementation for updating an enrolment
            var existingEnrolment = await _context.Enrolments.FindAsync(enrolmentId);
            if (existingEnrolment == null)
            {
                return new NotFoundResult(); // Return 404 if not found
            }
            // Update properties as needed
            await _context.SaveChangesAsync();
            return new OkResult(); // Return an appropriate result, e.g., Ok or NoContent
        }

        public async Task<IActionResult> DeleteEnrolment(int enrolmentId)
        {
            // Implementation for deleting an enrolment
            var enrolment = await _context.Enrolments.FindAsync(enrolmentId);
            if (enrolment == null)
            {
                return new NotFoundResult(); // Return 404 if not found
            }
            _context.Enrolments.Remove(enrolment);
            await _context.SaveChangesAsync();
            return new OkResult(); // Return an appropriate result, e.g., Ok or NoContent
        }
        }
}
