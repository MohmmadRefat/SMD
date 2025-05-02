using System.ComponentModel.DataAnnotations.Schema;

namespace SMD.Models
{
    public class StudentCourse:BaseEntity
    {
        public int? Grade { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public  Student? Student { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public  Course? Course { get; set; }
    }
}
