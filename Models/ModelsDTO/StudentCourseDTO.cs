using System.ComponentModel.DataAnnotations.Schema;

namespace SMD.Models.ModelsDTO
{
    public class StudentCourseDTO
    {
        public int StudentCourseId { get; set; }
        public double? Grade { get; set; }
        public string? Semester { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
