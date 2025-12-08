using CleanArchitecture.Application.Commands.Company;
using DomainCompany = CleanArchitecture.Domain.Entities.CompanyAggregate.Company;
using MediatR;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Domain.Entities.CompanyAggregate;

namespace CleanArchitecture.Application.CommandHandlers.Company
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Guid>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<CreateCompanyCommandHandler> _logger;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository, 
            ILogger<CreateCompanyCommandHandler> logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = DomainCompany.Create(
                request.Name, request.RegisterNumber, DateTime.Now);

            await _companyRepository.Add(company);
            await _companyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            _logger.LogInformation($"Company created successfully. Id: {company.Id}");

            return company.Id;
        }
    }
}
