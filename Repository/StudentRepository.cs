using SMD.Models;

namespace SMD.Repository
{
    public class StudentRepository: Repository<Student>
    {
        public StudentRepository(Context context) : base(context) { }
    }
}
