namespace SMD.Models.ModelsDTO
{
    public class StudentEnrollmentCourseDTO
    {

        public int StudentCourseId { get; set; }
        public string? CourseName { get; set; }
        public string? CourseCode { get; set; }
        public int CreditHours { get; set; }
        public int AcademicYear { get; set; }
        public double Grade { get; set; }
    }
}
