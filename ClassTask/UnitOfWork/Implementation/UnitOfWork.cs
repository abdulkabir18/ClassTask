using ClassTask.Context;
using ClassTask.UnitOfWork.Interface;

namespace ClassTask.UnitOfWork.Implementation
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
