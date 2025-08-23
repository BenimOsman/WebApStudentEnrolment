using System.ComponentModel.DataAnnotations;                                            // Provides validation attributes like [Required], [Key], etc.

namespace WebApStudentEnrolment.Models
{
    public class Course
    {
        [Key]
        public int Id { get;set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int Credits { get; set; }
        
        public Course() { }                                                             // Parameterless constructor (required for EF Core)
        
        public Course(int id, string name, string description, int credits)             // Constructor to initialize all fields
        {
            Id = id;
            Name = name;
            Description = description;
            Credits = credits;
        }

        // String representation of the course
        /*
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Description: {Description}, Credits: {Credits}";
        }
        */
    }
}