using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApStudentEnrolment.Data;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public class StudentRepo : IStudent                                 // This class implements the IStudent interface for managing student data.
                                                                        // It provides methods to add, retrieve, update, and delete student records.
    {
        private readonly StudentEnrolmentContext _context;              // Private readonly context to access the database

        public StudentRepo() { }
        public StudentRepo(StudentEnrolmentContext context)             // Constructor to access DB context
        {
            _context = context;
        }

        public int Count { get; private set; }                          // To count the number of students

        // Method - 1
        public async Task AddStudent(Student student)                   // Add a new student to the database
        {
            await _context.Students.AddAsync(student);                  // Add Student to DbSet
            await _context.SaveChangesAsync();                          // Save changes to DB
            return;                                                     // Return an appropriate result, e.g., Ok or Created
        }

        // Method - 2
        public async Task<Student> GetStudentById(int studentId)        // Get a single student by their ID
        {
            var student = await _context.Students.FindAsync(studentId); // Get existing student
            if (student == null)
            {
                return null;                                            // Return null if not found
            }
             return student;                                            // Return the found student object
        }

        // Method - 3
        public async Task<IEnumerable<Student>> GetAllStudents()        // Returns all students as a list
        {
            var students = await _context.Students.ToListAsync();       // Retrieves all records
            return students;                                            // Return the list of students
        }

        // Method - 4
        public async Task UpdateStudent(int id, Student student)        // Update student's information
        {
            var existingStudent = await _context.Students.FindAsync(id);// Get existing student
            if (existingStudent == null)
            {
                return;
            }

            existingStudent.Name = student.Name;                        // Updates individual fields
            existingStudent.Email = student.Email;
            existingStudent.Address = student.Address;
            _context.Students.Update(existingStudent);                  // Mark entity as modified
            await _context.SaveChangesAsync();                          // Save changes to DB
            
        }

        /*
        public async Task UpdateStudent(int id)
        {
            // Implementation for updating a student by ID
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return;
            }

            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            
        }
       */

        // Method - 5 
        public async Task DeleteStudent(int studentId)                  // Deletes a student by their ID
        {
            var student = await _context.Students.FindAsync(studentId); // Locate student
            if (student == null)
            {
                return; 
            }

            _context.Students.Remove(student);                          // Remove student           
            await _context.SaveChangesAsync();                          // Update changes
        }
    }
}