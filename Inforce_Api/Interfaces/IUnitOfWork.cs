namespace Inforce_Api.Interfaces
{
    public interface IUnitOfWork
    {
        public IShortenUrlRepository ShortenUrlRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
