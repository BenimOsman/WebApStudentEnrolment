using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public interface IStudent
    {
        int Count { get; }                                              // Get the count of total number of students
        // Task<IActionResult> AddStudent(int studentId, string name, string email, string address);
        
        Task AddStudent(Student student);                               // Method to add a student

        Task<Student?> GetStudentById(int studentId);                   // Method to add a specific student by their ID

        Task<IEnumerable<Student>> GetAllStudents();                    // Method to retrieve all the students
        
        Task UpdateStudent(int id,Student student);                     // Method to update a student based on their ID
        
        Task DeleteStudent(int studentId);                              // Method to delete a student by ID
    }
}