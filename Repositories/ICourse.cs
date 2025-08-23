using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public interface ICourse
    {
        int Count { get; }                                                      // Get the count of total number of students

        Task AddCourse(Course course);                                          // Method to add a new course

        Task<Course?> GetCourseById(int courseId);                              // Method to get a specific course by its ID

        Task<IEnumerable<Course>> GetAllCourses();                              // Method to retrieve all courses

        Task UpdateCourse(int courseId, Course course);                         // Method to update a course based on its ID

        Task DeleteCourse(int courseId);                                        // Method to delete a course by ID
    }
}