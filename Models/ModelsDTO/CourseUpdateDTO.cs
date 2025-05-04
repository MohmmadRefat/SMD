namespace SMD.Models.ModelsDTO
{
    public class CourseUpdateDTO
    {
        public int Id { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public int AcademicYear { get; set; }
        public int CreditHours { get; set; }
    }
}
