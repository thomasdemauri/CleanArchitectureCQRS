using MediatR;

namespace CleanArchitecture.Application.Commands.Company
{
    public record CreateCompanyCommand : IRequest<Guid>
    {
        public string Name { get; init; }
        public string RegisterNumber { get; init; }

        public CreateCompanyCommand(string name, string registerNumber)
        {
            Name = name;
            RegisterNumber = registerNumber;
        }
    }
}
