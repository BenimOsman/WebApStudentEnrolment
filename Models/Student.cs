using System.ComponentModel.DataAnnotations;                                    // Provides validation attributes like [Required], [Key], etc.

namespace WebApStudentEnrolment.Models
{
    public class Student
    {
        [Key]                                                                  // Marks 'Id' as Primary Key in the Db
        public int Id { get; set; }

        [Required]                                                             // Ensures that Name cannot be null or empty
        public string Name { get; set; } = string.Empty;

        [EmailAddress]                                                          // Validates that Email is in correct email format
        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public Student() { }                                                    // Parameterless constructor (required for EF Core)

        public Student(int id, string name, string email, string address)       // Constructor to initialize all fields
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
        }

        // Converts object to a readable string format
        /*
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Email: {Email}, Address: {Address}";
        }
        */
    }
}