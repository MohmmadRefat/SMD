using Microsoft.EntityFrameworkCore;

namespace SMD.Models
{
    public class Context : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<PrerequisiteCourses> Prerequisites { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<PrerequisiteCourses>()
                .HasOne(pc => pc.Course)
                .WithMany(c => c.PrerequisitesCourses)
                .HasForeignKey(pc => pc.CourseId);

            modelBuilder.Entity<PrerequisiteCourses>()
                 .HasOne(pc => pc.PrerequisiteCourse)
                 .WithMany(c => c.DependentCourses)
                 .HasForeignKey(pc => pc.PrerequisiteCourseId);

            modelBuilder.Entity<Student>().HasData(
      new Student { Id = 1, FullName = "John Doe", Email = "john.doe@example.com", AcademicYear = 1, Major = "Computer Science", PhoneNumber = "1234567890", GPA = 3.5, Semester = 1, TotalHours = 15 },
      new Student { Id = 2, FullName = "Jane Smith", Email = "jane.smith@example.com", AcademicYear = 2, Major = "Mathematics", PhoneNumber = "0987654321", GPA = 3.8, Semester = 2, TotalHours = 30 },
      new Student { Id = 3, FullName = "Alice Johnson", Email = "alice.johnson@example.com", AcademicYear = 3, Major = "Physics", PhoneNumber = "1122334455", GPA = 3.2, Semester = 3, TotalHours = 45 },
      new Student { Id = 4, FullName = "Bob Brown", Email = "bob.brown@example.com", AcademicYear = 4, Major = "Chemistry", PhoneNumber = "2233445566", GPA = 2.6, Semester = 4, TotalHours = 60 },
      new Student { Id = 5, FullName = "Charlie Green", Email = "charlie.green@example.com", AcademicYear = 1, Major = "Biology", PhoneNumber = "3344556677", GPA = 0, Semester = 1, TotalHours = 15 }
  );

            // Seed data for Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, CourseName = "Introduction to Programming", Degree = 100, CourseCode = "CS101", CreditHours = 3, ProfessorName = "Dr. Smith" },
                new Course { Id = 2, CourseName = "Calculus I", Degree = 100, CourseCode = "MATH101", CreditHours = 4, ProfessorName = "Dr. Johnson" },
                new Course { Id = 3, CourseName = "Physics I", Degree = 100, CourseCode = "PHYS101", CreditHours = 4, ProfessorName = "Dr. Brown" },
                new Course { Id = 4, CourseName = "Organic Chemistry", Degree = 100, CourseCode = "CHEM101", CreditHours = 3, ProfessorName = "Dr. Green" },
                new Course { Id = 5, CourseName = "Biology I", Degree = 100, CourseCode = "BIO101", CreditHours = 3, ProfessorName = "Dr. White" }
            );

            // Seed data for StudentCourses
            modelBuilder.Entity<StudentCourse>().HasData(
                new StudentCourse { Id = 1, StudentId = 1, CourseId = 1, Grade = 85 },
                new StudentCourse { Id = 2, StudentId = 2, CourseId = 2, Grade = 90 },
                new StudentCourse { Id = 3, StudentId = 3, CourseId = 3, Grade = 88 },
                new StudentCourse { Id = 4, StudentId = 4, CourseId = 4, Grade = 92 },
                new StudentCourse { Id = 5, StudentId = 5, CourseId = 5, Grade = 87 }
            );

            // Seed data for PrerequisiteCourses
            modelBuilder.Entity<PrerequisiteCourses>().HasData(
                new PrerequisiteCourses { Id = 1, CourseId = 2, PrerequisiteCourseId = 1 },
                new PrerequisiteCourses { Id = 2, CourseId = 3, PrerequisiteCourseId = 2 },
                new PrerequisiteCourses { Id = 3, CourseId = 4, PrerequisiteCourseId = 3 },
                new PrerequisiteCourses { Id = 4, CourseId = 5, PrerequisiteCourseId = 4 },
                new PrerequisiteCourses { Id = 5, CourseId = 1, PrerequisiteCourseId = 5 }
            );
        }
    }
}
