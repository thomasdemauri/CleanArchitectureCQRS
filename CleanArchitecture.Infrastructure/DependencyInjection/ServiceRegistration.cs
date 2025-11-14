using CleanArchitecture.Application;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infrastructure.Caching;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Queries;
using CleanArchitecture.Infrastructure.Queries.Resources;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace CleanArchitecture.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddTransient<IDbConnection>(sp =>
                new SqlConnection(configuration.GetConnectionString("Default")));

            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName = "redis-instance";
                options.Configuration = configuration.GetConnectionString("RedisDefault");
            });

            //services.AddDbContext<AppDbContext>(options =>
            //    options.UseNpgsql(configuration.GetConnectionString("Npgsql")));

            //services.AddTransient<IDbConnection>(sp =>
            //    new NpgsqlConnection(configuration.GetConnectionString("Npgsql")));

            services.AddTransient<QueriesResource>();
            services.AddTransient<CompanyQueries>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICachingService, CachingService>();

            return services;
        }
    }
}
