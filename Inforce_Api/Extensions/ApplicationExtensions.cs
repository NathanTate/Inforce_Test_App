using AutoMapper;
using FluentValidation;
using Inforce_Api.Data;
using Inforce_Api.Helpers;
using Inforce_Api.Interfaces;
using Inforce_Api.Services;
using Inforce_Api.Utility;
using Microsoft.EntityFrameworkCore;

namespace Inforce_Api.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            IMapper mapper = AutoMapperConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IUrlShorteningService, UrlShorteningService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

            services.AddMemoryCache();

            return services;
        }
    }
}
