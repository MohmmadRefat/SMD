using Microsoft.EntityFrameworkCore;
using SMD.Models;

namespace SMD.Repository
{
    public class Repository<Entity>:IRepository<Entity> where Entity : BaseEntity
    {
        public readonly Context _context;
        public readonly DbSet<Entity> _dbSet;
        public Repository(Context context)
        {
            _context = context;
            _dbSet = context.Set<Entity>();
        }
        public async Task<Entity?> AddAsync(Entity Entity)
        {
            await _dbSet.AddAsync(Entity);
            await _context.SaveChangesAsync();
            return Entity;
        }
        public async Task<Entity?> GetByIdAsync(int id)
        {
            return await _dbSet.Where(x=>x.Id == id && x.IsDeleted == false).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await _dbSet.Where(x=>x.IsDeleted==false).ToListAsync();
        }
        public async Task<bool> UpdateAsync(Entity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await _context.SaveChangesAsync(); // Persist changes to the database
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
