using SMD.Models;

namespace SMD.Repository
{
    public interface IRepository<Entity> where Entity : BaseEntity
    {
        Task<Entity?> GetByIdAsync(int id);
        Task<IEnumerable<Entity>> GetAllAsync();
        Task<Entity?> AddAsync(Entity entity);
        Task<bool> UpdateAsync(Entity entity);
        Task<bool> DeleteAsync(int id);
    }
}
