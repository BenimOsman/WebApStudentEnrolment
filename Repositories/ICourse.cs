using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public interface ICourse
    {
        int Count { get; }
        
        Task AddCourse(Course course);
        Task<Course?> GetCourseById(int courseId);
        Task<IEnumerable<Course>> GetAllCourses();
        Task UpdateCourse(int courseId, Course course);
        Task DeleteCourse(int courseId);
    }
}
