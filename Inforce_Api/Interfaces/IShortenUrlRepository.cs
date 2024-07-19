using FluentResults;
using Inforce_Api.Helpers;
using Inforce_Api.Models.DTO.ShortenUrl.Requests;
using Inforce_Api.Models.DTO.ShortenUrl.Responses;

namespace Inforce_Api.Interfaces
{
    public interface IShortenUrlRepository
    {
        Task<(string, int)> ShortenUrlAsync(ShortenUrlRequest model, string baseUrl, int userId);

        Task<Result<ShortenUrlResponse>> GetShortenUrlAsyncById(int id);
        Task<Result<ShortenUrlResponse>> GetShortenUrlAsync(string code);
        Task<Result> DeleteShortenUrlByIdAsync(int id, int userId);
        Task<Result> DeleteAllShortenUrlsAsync();
        Task<Result<PagedList<ShortenUrlResponse>>> GetAllShortenUrlsAsync(PaginationParams paginationParams);
    }
}
