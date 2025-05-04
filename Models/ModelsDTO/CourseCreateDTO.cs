namespace SMD.Models.ModelsDTO
{
    public class CourseCreateDTO
    {
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public int AcademicYear { get; set; }
        public int CreditHours { get; set; }
    }
}
