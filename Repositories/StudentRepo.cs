using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApStudentEnrolment.Data;

using WebApStudentEnrolment.Models;
namespace WebApStudentEnrolment.Repositories
{
    public class StudentRepo : IStudent
    {
        // This class implements the IStudent interface for managing student data.
        // It provides methods to add, retrieve, update, and delete student records.

        private readonly StudentEnrolmentContext _context;

        public StudentRepo() { }

        public StudentRepo(StudentEnrolmentContext context)
        {
            _context = context;
        }
        public int Count { get; private set; }

        public async Task AddStudent(Student student)
        {
            // Implementation for adding a student
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            
            return; // Return an appropriate result, e.g., Ok or Created
            
        }

        public async Task<Student> GetStudentById(int studentId)
        {
            // Implementation for retrieving a student by ID
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                return null; // Return null if not found
                //return new NotFoundResult(); // Alternatively, you can return a NotFoundResult
            }
             return student; // Return the student object
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            // Implementation for retrieving all students
            var students = await _context.Students.ToListAsync();
            // Return the list of students
            return students; // Return the list of students
        }

        public async Task UpdateStudent(int id, Student student)
        {
            // Implementation for updating a student
            var existingStudent = await _context.Students.FindAsync(id);
            if (existingStudent == null)
            {
                return;
               // return new NotFoundResult(); // Return 404 if not found
            }
            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            existingStudent.Address = student.Address;
            _context.Students.Update(existingStudent);
            await _context.SaveChangesAsync();
            
        }

       /* public async Task UpdateStudent(int id)
        {
            // Implementation for updating a student by ID
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return;
            }
            // Here you can update the student's properties as needed
            // For example, student.Name = "New Name";
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            
        }*/

        public async Task DeleteStudent(int studentId)
        {
            // Implementation for deleting a student by ID
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                return; 
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            
        }

    }
}
