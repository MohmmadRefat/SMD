using System.ComponentModel.DataAnnotations;

namespace SMD.Models.ModelsDTO
{
    public class CourseUpdateDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Course Code is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string? CourseCode { get; set; }
        [Required(ErrorMessage = "Course Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string? CourseName { get; set; }
        [Range(1, 4, ErrorMessage = "Invaild Academic Year")]
        public int AcademicYear { get; set; }
        [Range(0, 6, ErrorMessage = "Credit Hours must between 1 and 6")]
        public int CreditHours { get; set; }
    }
}
