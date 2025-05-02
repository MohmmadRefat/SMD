using System.ComponentModel.DataAnnotations;

namespace SMD.Models.ModelsDTO
{
    public class StudentUpdateDTO
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Range(1, 8, ErrorMessage = "Academic Year must be between 1 and 8.")]
        public int AcademicYear { get; set; }

        [Required(ErrorMessage = "Major is required.")]
        [StringLength(50, ErrorMessage = "Major cannot exceed 50 characters.")]
        public string? Major { get; set; }

        [Range(0, 4, ErrorMessage = "Invalid GPA.")]
        public double? GPA { get; set; }

        [Range(0, 18, ErrorMessage = "Invalid TotalHours.")]
        public int? TotalHours { get; set; }

        [Range(1900, 2025, ErrorMessage = "Enrollment Year must be between 1900 and 2025.")]
        public int? EnrollmentYear { get; set; }
    }
}
