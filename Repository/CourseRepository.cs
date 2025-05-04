using SMD.Models;

namespace SMD.Repository
{
    public class CourseRepository : Repository<Course>
    {
        public CourseRepository(Context context) : base(context)
        {
        }
    }
}
