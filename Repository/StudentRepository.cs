using Microsoft.EntityFrameworkCore;
using SMD.Models;

namespace SMD.Repository
{
    public class StudentRepository: IRepository<Student>
    {
        public readonly Context _context;
        public  DbSet<Student> _dbSet;
        public StudentRepository(Context context)
        {
            _context = context;
            _dbSet = context.Set<Student>();
        }

        public async Task<Student?> AddAsync(Student Entity)
        {
            await _dbSet.AddAsync(Entity);
            await _context.SaveChangesAsync();
            return Entity;
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id&&x.IsDeleted != true);

        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _dbSet.Where(x =>x.IsDeleted!=true).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Student entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if(student != null)
            {
                student.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
