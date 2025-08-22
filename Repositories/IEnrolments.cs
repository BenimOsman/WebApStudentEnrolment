using Microsoft.AspNetCore.Mvc;
using WebApStudentEnrolment.Models;

namespace WebApStudentEnrolment.Repositories
{
    public interface IEnrolments
    {
        int Count { get; }
        // Task<IActionResult> AddEnrolment(int enrolmentId, int studentId, int courseId, DateTime enrolmentDate);
        Task<IActionResult> AddEnrolment(Enrolment enrolment);
        Task<IActionResult> GetEnrolmentById(int enrolmentId);
        Task<IActionResult> GetAllEnrolments();
        Task<IActionResult> UpdateEnrolment(Enrolment enrolment);
        Task<IActionResult> DeleteEnrolment(int enrolmentId);
    }
}
