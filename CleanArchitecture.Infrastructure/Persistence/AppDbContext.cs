using CleanArchitecture.Application;
using CleanArchitecture.Domain.Entities.CompanyAggregate;
using CleanArchitecture.Domain.Entities.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence
{

    public class AppDbContext : DbContext, IUnitOfWork
    {
        private readonly ILogger<AppDbContext> _logger;
        public AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger) : base(options)
        {
            _logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Log das entidades rastreadas antes de salvar
            var entries = ChangeTracker.Entries()
                .Select(e => new {
                    Type = e.Entity.GetType().Name,
                    State = e.State.ToString(),
                    KeyValues = e.Properties
                                 .Where(p => p.Metadata.IsPrimaryKey())
                                 .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue)
                })
                .ToList();

            _logger.LogInformation("ChangeTracker entries before SaveChanges: {@entries}", entries);

            // Opcional: log de contratos locais
            var localContracts = Set<Contract>().Local.ToList();
            _logger.LogInformation("Local Contracts: {@localContracts}", localContracts);

            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Contract> Contracts => Set<Contract>();

    }
}
