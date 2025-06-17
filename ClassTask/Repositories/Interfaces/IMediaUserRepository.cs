using ClassTask.Models;
using System.Linq.Expressions;

namespace ClassTask.Repositories.Interfaces
{
    public interface IMediaUserRepository
    {
        Task AddAsync(MediaUser mediaUser);
        void Update(MediaUser mediaUser);
        void Delete(MediaUser mediaUser);
        Task<MediaUser?> GetAsync(Guid id);
        Task<bool> CheckAsync(Expression<Func<MediaUser, bool>> expression);
        Task<MediaUser?> GetAsync(Expression<Func<MediaUser, bool>> expression);
        Task<IEnumerable<MediaUser>> GetAllAsync(Expression<Func<MediaUser, bool>> expression);
        Task<IEnumerable<MediaUser>> GetAllAsync();
    }
}
