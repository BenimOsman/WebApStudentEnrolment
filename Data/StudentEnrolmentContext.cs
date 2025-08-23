using Microsoft.EntityFrameworkCore;                                    // EF Core Functionality for Working with Databases.

namespace WebApStudentEnrolment.Data
{
    public class StudentEnrolmentContext : DbContext
    {
        // Constructor that takes in options like the connection string, passed during app startup
        // Pass the options to the DbContext class
        public StudentEnrolmentContext(DbContextOptions<StudentEnrolmentContext> options) : base(options) { }

        // Represents the Students table in the database
        public DbSet<Models.Student> Students { get; set; } = null!;

        // Represents the Courses table in the database
        public DbSet<Models.Course> Courses { get; set; } = null!;

        // Represents the Enrolments table in the database
        public DbSet<Models.Enrolment> Enrolments { get; set; } = null!;

        /*
        // Optional: used to customize the model relationships, table names, constraints, etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);                         // Call the base configuration
                                                                        // You can add custom Fluent API configurations here in the future if needed
        }
        */
    }
}

// DbSet<T>: Tells EF Core to create and manage a table for that model.