using Inforce_Api.Data;
using Inforce_Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inforce_Api.Services
{
    public class UrlShorteningService : IUrlShorteningService
    {
        private const int AmountOfCharsInShortLink = 10;
        private const string Characters = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm123456789";
        
        private readonly Random _random;
        private readonly AppDbContext _dbContext;

        public UrlShorteningService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _random = new Random();
        }
        public async Task<string> GenerateUniqueCode()
        {
            var codeCharArray = new char[AmountOfCharsInShortLink];

            while (true)
            {
                for (int i = 0; i < codeCharArray.Length; i++)
                {
                    var randomIndex = _random.Next(Characters.Length);

                    codeCharArray[i] = Characters[randomIndex];
                }

                var code = new string(codeCharArray);

                if(!await _dbContext.ShortenUrls.AnyAsync(u => u.Code == code))
                {
                    return code;
                }
            }


        }
    }
}
