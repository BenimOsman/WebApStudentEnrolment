using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public interface IStudent
    {
        int Count { get; }
        //Task<IActionResult> AddStudent(int studentId, string name, string email, string address);
        Task AddStudent(Student student);
        Task<Student?> GetStudentById(int studentId);
        Task<IEnumerable<Student>> GetAllStudents();
        
        Task UpdateStudent(int id,Student student);
        Task DeleteStudent(int studentId);
    }
}