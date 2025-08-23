using Microsoft.AspNetCore.Mvc;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public interface IEnrolments
    {
        int Count { get; }                                                          // Get the count of total number of students

        // Task<IActionResult> AddEnrolment(int enrolmentId, int studentId, int courseId, DateTime enrolmentDate);

        Task<IActionResult> AddEnrolment(Enrolment enrolment);                      // Method to add a new enrolment

        Task<IActionResult> GetEnrolmentById(int enrolmentId);                      // Method to get a specific enrolment by its ID

        Task<IActionResult> GetAllEnrolments();                                     // Method to retrieve all enrolments

        Task<IActionResult> UpdateEnrolment(Enrolment enrolment);                   // Method to update an existing enrolment

        Task<IActionResult> DeleteEnrolment(int enrolmentId);                       // Method to delete an enrolment by ID
    }
}