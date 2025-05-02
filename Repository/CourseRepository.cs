using Microsoft.EntityFrameworkCore;
using SMD.Models;

namespace SMD.Repository
{
    public class CourseRepository : IRepository<Course>
    {
        public readonly Context _context;
        public DbSet<Course> _dbSet;

        public CourseRepository(Context context)
        {
            _context = context;
            _dbSet = context.Set<Course>();
        }
        public Task<Course?> AddAsync(Course entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Course>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Course?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Course entity)
        {
            throw new NotImplementedException();
        }
    }
}
