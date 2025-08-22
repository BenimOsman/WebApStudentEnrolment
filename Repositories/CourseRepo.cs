using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApStudentEnrolment.Data;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public class CourseRepo :ICourse
    {
        // This class implements the ICourse interface for managing course data.
        // It provides methods to add, retrieve, update, and delete course records.
        private readonly StudentEnrolmentContext _context;
        public CourseRepo() { }
        public CourseRepo(StudentEnrolmentContext context)
        {
            _context = context;
        }
        public int Count { get; private set; }
        // Implement methods for adding, retrieving, updating, and deleting courses here
    
       public async Task AddCourse(Course course)
        {
            // Implementation for adding a course
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            
        }
        public async Task<Course> GetCourseById(int courseId)
        {
            // Implementation for retrieving a course by ID
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                return null;
            }
            return course; // Return the course object
        }
        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            // Implementation for retrieving all courses
            var courses = await _context.Courses.ToListAsync();
            
            return courses; // Return the list of courses
        }
        public async Task UpdateCourse(int courseId, Course course)
        {
            // Implementation for updating a course
            var existingCourse = await _context.Courses.FindAsync(courseId);
            if (existingCourse == null)
            {
                return;
            }
            // Update properties as needed
            existingCourse.Name = course.Name;
            existingCourse.Description = course.Description;
            existingCourse.Credits = course.Credits;
            _context.Courses.Update(existingCourse);

            await _context.SaveChangesAsync();
            
        }
        public async Task DeleteCourse(int courseId)
        {
            // Implementation for deleting a course
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                return; // Return 404 if not found
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            //return ; // Return an appropriate result, e.g., Ok or NoContent
        }
    }
}
