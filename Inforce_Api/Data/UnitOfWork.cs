using AutoMapper;
using Inforce_Api.Data.Repositories;
using Inforce_Api.Interfaces;

namespace Inforce_Api.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;
        private readonly IUrlShorteningService _urlShorteningService;
        public UnitOfWork(IMapper mapper, AppDbContext dbContext, IUrlShorteningService urlShorteningService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _urlShorteningService = urlShorteningService;

        }

        public IShortenUrlRepository ShortenUrlRepository => new ShortenUrlRepository(_dbContext, _urlShorteningService, _mapper);

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
