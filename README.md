# StudentWebProject
Basic Code for Students Webpage using ASP .NET.


	## STEPS THAT WERE TAKEN:
		1. CREATE A MVC .NET CORE PROJECT:
			Created a new ASP.NET MVC Core Student Project by selecting VS Code Template.
		
		2. CREATE MODEL CLASSES: 
			Created a Student, Course, Enrollment Model classes (with different attributes like StudentId, Name, Email, Address, etc. [also applied data annotations like [Required], [Key], [StringLength], [Foreign Key] for validation]
		
		3. INSTALL REQUIRED NuGet Packages: 
			Install the following Entity Framework packages - 
				Microsoft.EntityFrameworkCore / Sqlserver / Tools
		
		4. CONFIGURE DbContext: 
			Then, created a Data Folder and inside this create a StudentEnrolmentContext.cs [DbContext file] [For configuring SQL Server Connection String].
		
		5. CONFIGURE Connection String: 
			Open appSettings.json and update the connection string to include the DbName and Db location to be taken.
		
		6. REGISTER DbContext:
			Register DbContext in Program.cs using the AddDbContext() and connection string.
		
		7. Define Interfaces:
			Create interfaces (IStudent, ICourse, IEnrolments) defining repository method.
			
		8. IMPLEMENT REPOSITORIES CLASSES:
			Create StudentRepo.cs, CourseRepo.cs, EnrolmentRepo.cs inside Repositories.
			Implement each interface and inject StudentEnrolmentContext for DB operations.
			
		9. REGISTER REPOSITORIES IN Program.cs:
			In Program.cs, register each repo for dependency injection:
			
		10. CREATE CONTROLLERS USING DEPENDENCY INJECTION:
			Create 3 controllers manually: StudentController.cs, CourseController.cs, EnrolmentController.cs and Inject interfaces.
			Use methods from repo instead of directly using DbContext.
			
		11. CREATE RAZOR VIEWS:
			Add Razor views (Index, Create, Edit, Details, Delete) for each controller manually or by scaffolding.
			Use @model binding and Bootstrap for layout.
		
		12. EDIT LAYOUT NAVIGATION:
			Update _Layout.cshtml to add navbar links to Student, Course, and Enrolment pages.
			
		13. CREATE DATABASE VIA MIGRATIONS:
			Add-Migration "Initial Creations"
			Update Database
RUN AND TEST APP:<img width="880" height="1120" alt="image" src="https://github.com/user-attachments/assets/58a8c21d-6ce9-4176-a16f-e7cd0f44e8a6" />
