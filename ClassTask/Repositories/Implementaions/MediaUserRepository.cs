using ClassTask.Context;
using ClassTask.Models;
using ClassTask.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClassTask.Repositories.Implementaions
{
    public class MediaUserRepository : IMediaUserRepository
    {
        private readonly AppDbContext _contect;
        public MediaUserRepository(AppDbContext context)
        {
            _contect = context;
        }
        public async Task AddAsync(MediaUser mediaUser)
        {
            await _contect.MediaUsers.AddAsync(mediaUser);
        }

        public Task<bool> CheckAsync(Expression<Func<MediaUser, bool>> expression)
        {
            return _contect.MediaUsers.AnyAsync(expression);
        }

        public void Delete(MediaUser mediaUser)
        {
            _contect.MediaUsers.Remove(mediaUser);
        }

        public  async Task<IEnumerable<MediaUser>> GetAllAsync(Expression<Func<MediaUser, bool>> expression)
        {
            return await _contect.MediaUsers.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<MediaUser>> GetAllAsync()
        {
            return await _contect.MediaUsers.ToListAsync();
        }

        public async Task<MediaUser?> GetAsync(Guid id)
        {
            return await _contect.MediaUsers.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<MediaUser?> GetAsync(Expression<Func<MediaUser, bool>> expression)
        {
            return await _contect.MediaUsers.FirstOrDefaultAsync(expression);
        }

        public void Update(MediaUser mediaUser)
        {
            _contect.Update(mediaUser);
        }
    }
}
