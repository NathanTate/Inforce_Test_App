using FluentResults;
using FluentValidation;
using Inforce_Api.Extensions;
using Inforce_Api.Helpers;
using Inforce_Api.Interfaces;
using Inforce_Api.Models.DTO.ShortenUrl.Requests;
using Inforce_Api.Models.DTO.ShortenUrl.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;
using static Inforce_Api.Utility.SD;

namespace Inforce_Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ShortenUrlController : BaseApiController
    {
        private readonly HttpContext _httpContext;
        private readonly IUnitOfWork _uow;
        private readonly IMemoryCache _memoryCache;
        public ShortenUrlController(IUnitOfWork uow, IHttpContextAccessor httpContextAccessor, IMemoryCache memoryCache)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _uow = uow;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        public async Task<ActionResult<string>> ShortenUrl(ShortenUrlRequest model, [FromServices] IValidator<ShortenUrlRequest> validator)
        {
            ModelStateDictionary errors = ValidateModel.Validate(validator, model);

            if (errors.Count > 0)
            {
                return ValidationProblem(errors);
            }

            if (!Uri.TryCreate(model.LongUrl, UriKind.Absolute, out _))
            {
                return BadRequest("Invalid Url");
            }


            string baseUrl = $"{_httpContext.Request.Scheme}://{HttpContext.Request.Host}/api/ShortenUrl";
            
            var result = await _uow.ShortenUrlRepository.ShortenUrlAsync(model, baseUrl, User.GetUserId());

            await _uow.SaveChangesAsync();

            _memoryCache.Remove($"getAll-1-10");

            return CreatedAtAction(nameof(ShortenUrl), new { Id = result.Item2, shortUrl = result.Item1, Version = "1.0" });
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetUrl/{Id}")]
        public async Task<ActionResult<ShortenUrlResponse>> GetShortenUrl(int Id)
        {
            ShortenUrlResponse shortenUrl = null!;
            string key = $"shortenUrl-{Id}";

            if (!_memoryCache.TryGetValue(key, out shortenUrl))
            {
                Result<ShortenUrlResponse> response = await _uow.ShortenUrlRepository.GetShortenUrlAsyncById(Id);
                if (response.IsFailed)
                {
                    return NotFound(response.Errors[0]);
                }

                shortenUrl = response.Value;

                AddCache(key, shortenUrl);
            }
            
            return Ok(shortenUrl);
        }

        [AllowAnonymous]
        [HttpGet("{code}")]
        public async Task<IActionResult> RedirectToUrl(string code)
        {
            ShortenUrlResponse shortenUrl = null!;

            if (!_memoryCache.TryGetValue(code, out shortenUrl))
            {
                Result<ShortenUrlResponse> response = await _uow.ShortenUrlRepository.GetShortenUrlAsync(code);
                if (response.IsFailed)
                {
                    return NotFound(response.Errors[0]);
                }

                shortenUrl = response.Value;

                AddCache(code, shortenUrl);
            }



            return Redirect(shortenUrl.LongUrl);
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllShortenUrls([FromQuery] PaginationParams paginationParams)
        {
            PagedList<ShortenUrlResponse> shortenUrls = null;
            string key = $"getAll-{paginationParams.Page}-{paginationParams.PageSize}";

            if (!_memoryCache.TryGetValue(key, out shortenUrls))
            {
                var result = await _uow.ShortenUrlRepository.GetAllShortenUrlsAsync(paginationParams);

                shortenUrls = result.Value;

                AddCache(key, shortenUrls);
            }

            return Ok(shortenUrls);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteShortenUrl(int id)
        {
            var result = await _uow.ShortenUrlRepository.DeleteShortenUrlByIdAsync(id, User.GetUserId());

            if(result.IsFailed)
            {
                return NotFound(result.Errors[0]);
            }

            await _uow.SaveChangesAsync();

            _memoryCache.Remove($"getAll-1-10");

            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = nameof(UserRoles.ADMIN))]
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAllShortenUrls()
        {
            var result = await _uow.ShortenUrlRepository.DeleteAllShortenUrlsAsync();

            if(result.IsFailed)
            {
                return BadRequest("Problem during deletion");
            }

            _memoryCache.Remove($"getAll-1-10");

            return Ok();
        }

        private void AddCache<T>(string key, T cache)
        {
            var options = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(30),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(50)
            };

            _memoryCache.Set<T>(key, cache, options);
        }
    }
}
