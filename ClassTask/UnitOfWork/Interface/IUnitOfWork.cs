namespace ClassTask.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
