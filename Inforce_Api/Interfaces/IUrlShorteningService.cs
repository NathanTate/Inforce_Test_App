using Inforce_Api.Models;

namespace Inforce_Api.Interfaces
{
    public interface IUrlShorteningService
    {
        Task<string> GenerateUniqueCode();
    }
}
