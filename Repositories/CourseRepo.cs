using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApStudentEnrolment.Data;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public class CourseRepo : ICourse                                                   // This class implements the ICourse interface for managing course data.
                                                                                        // It provides methods to add, retrieve, update, and delete course records.
    {
        private readonly StudentEnrolmentContext _context;                              // To hold the database context
        
        public CourseRepo() { }
        public CourseRepo(StudentEnrolmentContext context)                              // Constructor with dependency injection to get the DB context
        {
            _context = context;
        }

        public int Count { get; private set; }                                          // To count the number of courses
    
        public async Task AddCourse(Course course)                                      // Adds a new course to the database
        {
            await _context.Courses.AddAsync(course);                                    // Adds the course to DbSet
            await _context.SaveChangesAsync();                                          // Commits changes to database
        }

        public async Task<Course> GetCourseById(int courseId)                           // Retrieves a specific course by its ID
        {
            var course = await _context.Courses.FindAsync(courseId);                    // Looks for the course by primary key
            
            if (course == null)
            {
                return null;
            }
            return course;                                                              // Return the course object
        }

        public async Task<IEnumerable<Course>> GetAllCourses()                          // Retrieves all courses from the database
        {
            var courses = await _context.Courses.ToListAsync();                         // Converts results to a list
            return courses;                                                             // Return the list of courses
        }

        public async Task UpdateCourse(int courseId, Course course)                     // Updates an existing course's details
        {
            var existingCourse = await _context.Courses.FindAsync(courseId);            // Finds existing course

            if (existingCourse == null)
            {
                return;
            }

            existingCourse.Name = course.Name;                                          // Updates each properties
            existingCourse.Description = course.Description;
            existingCourse.Credits = course.Credits;                                    
            _context.Courses.Update(existingCourse);                                    // Marks the course as modified
            await _context.SaveChangesAsync();                                          // Save changes to the DB
        }

        public async Task DeleteCourse(int courseId)                                    // Deletes a course by its ID
        {
            var course = await _context.Courses.FindAsync(courseId);                    // Finds the course
            if (course == null)
            {
                return; 
            }

            _context.Courses.Remove(course);                                            // Remove it from the DbSet
            await _context.SaveChangesAsync();                                          // Commits the deletion
        }
    }
}