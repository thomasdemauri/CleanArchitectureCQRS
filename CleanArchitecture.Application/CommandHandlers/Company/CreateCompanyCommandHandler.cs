using CleanArchitecture.Application.Commands.Company;
using DomainCompany = CleanArchitecture.Domain.Entities.CompanyAggregate.Company;
using MediatR;
using CleanArchitecture.Application.Interfaces;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Application.IntegrationEvents.Company;
using CleanArchitecture.Application.Abstractions.Events;

namespace CleanArchitecture.Application.CommandHandlers.Company
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Guid>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<CreateCompanyCommandHandler> _logger;
        private readonly IEventBus _eventBus;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository, 
            ILogger<CreateCompanyCommandHandler> logger,
            IEventBus eventBus)
        {
            _companyRepository = companyRepository;
            _logger = logger;
            _eventBus = eventBus;
        }

        public async Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = DomainCompany.Create(
                request.Name, request.RegisterNumber, DateTime.Now);

            await _companyRepository.Add(company);
            await _companyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            await _eventBus.PublishAsync(
                new CompanyCreatedIntegrationEvent
                (Guid.NewGuid(), company.Name, company.RegisterNumber, company.EstablishedOn));

            _logger.LogInformation($"Company created successfully. Id: {company.Id}");

            return company.Id;
        }
    }
}
