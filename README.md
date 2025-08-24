# StudentWebProject

Basic ASP.NET Core MVC project for managing students, courses, and enrolments.

---

## Project Overview

This project demonstrates a simple web application built with ASP.NET Core MVC. It allows users to manage Students, Courses, and Enrolments with CRUD operations, using Entity Framework Core for database interaction.

---

## Steps Taken to Build the Project

1. **Create MVC .NET Core Project**  
   Created a new ASP.NET Core MVC project using the Visual Studio template.

2. **Create Model Classes**  
   Developed `Student`, `Course`, and `Enrolment` model classes, including attributes such as `StudentId`, `Name`, `Email`, `Address`, etc.  
   Applied data annotations like `[Required]`, `[Key]`, `[StringLength]`, and `[ForeignKey]` for validation and relationships.

3. **Install Required NuGet Packages**  
   Installed necessary Entity Framework Core packages:  
   - `Microsoft.EntityFrameworkCore`  
   - `Microsoft.EntityFrameworkCore.SqlServer`  
   - `Microsoft.EntityFrameworkCore.Tools`

4. **Configure DbContext**  
   Created a `Data` folder and added `StudentEnrolmentContext.cs` inheriting from `DbContext` to configure database context and connection.

5. **Configure Connection String**  
   Updated `appsettings.json` with the SQL Server connection string specifying the database name and location.

6. **Register DbContext**  
   Registered the DbContext service in `Program.cs` using `AddDbContext()` method along with the connection string.

7. **Define Repository Interfaces**  
   Created interfaces (`IStudent`, `ICourse`, `IEnrolments`) that define repository methods for abstraction and loose coupling.

8. **Implement Repository Classes**  
   Developed `StudentRepo.cs`, `CourseRepo.cs`, and `EnrolmentRepo.cs` implementing the repository interfaces and injecting `StudentEnrolmentContext` for data operations.

9. **Register Repositories for Dependency Injection**  
   Registered repository implementations in `Program.cs` for Dependency Injection.

10. **Create Controllers with Dependency Injection**  
    Created `StudentController.cs`, `CourseController.cs`, and `EnrolmentController.cs` manually, injecting repository interfaces and utilizing repository methods instead of direct DbContext access.

11. **Create Razor Views**  
    Added Razor views (`Index`, `Create`, `Edit`, `Details`, `Delete`) for each controller, using model binding and Bootstrap for UI layout and styling.

12. **Update Layout Navigation**  
    Modified `_Layout.cshtml` to include navigation links to Student, Course, and Enrolment pages.

13. **Create Database via Migrations**  
    Ran Entity Framework migrations:  
    ```bash
    Add-Migration "Initial Creations"
    Update-Database
    ```

14. **Run and Test the Application**  
    Built and tested the application to verify functionality.

---

## Technologies Used

- ASP.NET Core MVC  
- Entity Framework Core  
- SQL Server  
- Razor Views  
- Bootstrap (for styling)

---

Feel free to contribute or raise issues!

---

**Contributors:** Abdul Yesdani, Syed Faisal  
**Date:** 23-08-2025
