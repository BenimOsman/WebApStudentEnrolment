
using System.ComponentModel.DataAnnotations;

namespace WebApStudentEnrolment.Models
{
    public class Enrolment
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrolmentDate { get; set; }

        public virtual Student? Student { get; set; }

        public virtual Course? Course { get; set; }

        public Enrolment() { }
        public Enrolment(int id, int studentId, int courseId, DateTime enrolmentDate)
        {
            Id = id;
            StudentId = studentId;
            CourseId = courseId;
            EnrolmentDate = enrolmentDate;
        }
        public override string ToString()
        {
            return $"Id: {Id}, StudentId: {StudentId}, CourseId: {CourseId}, EnrolmentDate: {EnrolmentDate}" +
                $"Student Nme :{ Student.Name} Course Title: {Course.Name}" ;
        }
    }
}
