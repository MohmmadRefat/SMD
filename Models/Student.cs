namespace SMD.Models
{
    public class Student:BaseEntity
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int AcademicYear { get; set; }   
        public string? Major { get; set; }
        public string? PhoneNumber { get; set; }
        public double? GPA { get; set; }
        public int? Semester { get; set; }
        public int? TotalHours { get; set; }
        public int? EnrollmentYear { get; set; }
        public ICollection<StudentCourse>? Courses { get; set; }
    }
}
