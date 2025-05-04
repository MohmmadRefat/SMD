namespace SMD.Models
{
    public class Course:BaseEntity
    {
        public string? CourseName { get; set; }
        public int Degree { get; set; }
        public string? CourseCode { get; set; }
        public int CreditHours { get; set; }
        public string? ProfessorName { get; set; }
        public int AcademicYear { get; set; }
        public ICollection<StudentCourse>? Students { get; set; }
        public ICollection<PrerequisiteCourses>? PrerequisitesCourses { get; set; }
        public ICollection<PrerequisiteCourses>? DependentCourses { get; set; }
    }
}
