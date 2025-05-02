namespace SMD.Models.ModelsDTO
{
    public class StudentDetailsDTO
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int AcademicYear { get; set; }
        public string? Major { get; set; }
        public double? GPA { get; set; }
        public int? TotalHours { get; set; }
        public int? EnrollmentYear { get; set; }
    }
}
