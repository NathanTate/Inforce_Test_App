using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentResults;
using Inforce_Api.Helpers;
using Inforce_Api.Interfaces;
using Inforce_Api.Models;
using Inforce_Api.Models.DTO.ShortenUrl.Requests;
using Inforce_Api.Models.DTO.ShortenUrl.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using static Inforce_Api.Utility.SD;

namespace Inforce_Api.Data.Repositories
{
    public class ShortenUrlRepository : IShortenUrlRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IUrlShorteningService _urlShorteningService;
        private readonly IMapper _mapper;
        public ShortenUrlRepository(AppDbContext dbContext, IUrlShorteningService urlShorteningService, IMapper mapper)
        {
            _dbContext = dbContext;
            _urlShorteningService = urlShorteningService;
            _mapper = mapper;
        }

        public async Task<(string, int)> ShortenUrlAsync(ShortenUrlRequest model, string baseUrl, int userId)
        {
            var code = await _urlShorteningService.GenerateUniqueCode();

            var shortenUrl = new ShortenUrl
            {
                LongUrl = model.LongUrl,
                Code = code,
                ShortUrl = $"{baseUrl}/{code}",
                CreatedAt = DateTime.UtcNow,
                ApplicationUserId = userId
            };
            try
            {
                _dbContext.ShortenUrls.Add(shortenUrl);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                throw;
            }

            return (shortenUrl.ShortUrl, shortenUrl.Id);
        }

        public async Task<Result<ShortenUrlResponse>> GetShortenUrlAsyncById(int id)
        {
            var shortenUrl = _mapper.Map<ShortenUrlResponse>(await _dbContext.ShortenUrls.FindAsync(id));

            if(shortenUrl == null)
            {
                return Result.Fail("Url not found");
            }

            return Result.Ok(shortenUrl);
        }


        public async Task<Result<ShortenUrlResponse>> GetShortenUrlAsync(string code)
        {
            var shortenUrl = _mapper.Map<ShortenUrlResponse>(await _dbContext.ShortenUrls.FirstOrDefaultAsync(x => x.Code == code));

            if (shortenUrl == null)
            {
                return Result.Fail("Url not found");
            }

            return Result.Ok(shortenUrl);
        }

        public async Task<Result> DeleteShortenUrlByIdAsync(int id, int userId)
        {
            var shortenUrl = await _dbContext.ShortenUrls.Where(u => u.ApplicationUserId == userId).FirstOrDefaultAsync();

            if(shortenUrl == null)
            {
                return Result.Fail("Url not found");
            }

            _dbContext.ShortenUrls.Remove(shortenUrl);

            return Result.Ok();
        }

        public async Task<Result> DeleteAllShortenUrlsAsync()
        {
            await _dbContext.ShortenUrls.ExecuteDeleteAsync();

            return Result.Ok();
        }

        public async Task<Result<PagedList<ShortenUrlResponse>>> GetAllShortenUrlsAsync(PaginationParams paginationParams)
        {
            IQueryable<ShortenUrl> shortenUrlsQuery = _dbContext.ShortenUrls.AsNoTracking();

            shortenUrlsQuery = shortenUrlsQuery.OrderByDescending(x => x.Id);

            var result = await PagedList<ShortenUrlResponse>.CreateAsync(
                shortenUrlsQuery.ProjectTo<ShortenUrlResponse>(_mapper.ConfigurationProvider),
                paginationParams.Page,
                paginationParams.PageSize);

            return result;
        }
    }
}
