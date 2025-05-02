using System.ComponentModel.DataAnnotations.Schema;

namespace SMD.Models
{
    public class PrerequisiteCourses : BaseEntity
    {
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public  Course? Course { get; set; }

        [ForeignKey(nameof(PrerequisiteCourse))]
        public int PrerequisiteCourseId { get; set; }
        public Course? PrerequisiteCourse { get; set; }
    }
}
