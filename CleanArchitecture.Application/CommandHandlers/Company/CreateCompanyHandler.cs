using CleanArchitecture.Application.Commands.Company;
using DomainCompany = CleanArchitecture.Domain.Entities.Company;
using MediatR;
using CleanArchitecture.Application.Interfaces;

namespace CleanArchitecture.Application.CommandHandlers.Company
{
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, Guid>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCompanyHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = DomainCompany.Create(
                request.Name, request.RegisterNumber, DateTime.Now);

            await _companyRepository.Add(company);
            await _unitOfWork.SaveChangesAsync();

            return company.Id;
        }
    }
}
