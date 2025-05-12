using AspNetCoreGeneratedDocument;
using Microsoft.EntityFrameworkCore;
using SMD.Models;

namespace SMD.Repository
{
    public class StudentCourseRepository : Repository<StudentCourse>
    {
        public StudentCourseRepository(Context context) : base(context)
        {
        }
            
        public async Task<bool> RemoveStudentCourseAsync(int studentId,int courseId)
        {
            var entity= await _dbSet.FirstOrDefaultAsync(SC=>SC.CourseId==courseId&&SC.StudentId==studentId);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync(); // Persist changes to the database
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<StudentCourse>?GetStudentCourse(int studentId,int courseId)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(SC => SC.CourseId == courseId && SC.StudentId == studentId);
            return entity;
        }

    }
}
