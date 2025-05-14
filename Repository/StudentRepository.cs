using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMD.Models;

namespace SMD.Repository
{
    public class StudentRepository: Repository<Student>
    {
        public StudentRepository(Context context) : base(context) { }

        public async Task<Student> GetStudentWithStudnetCourseAsync(int studentId)
        {
            return await  _dbSet.Include(s => s.Courses)
                .FirstOrDefaultAsync(i=> i.Id == studentId);
        }
    }
}
