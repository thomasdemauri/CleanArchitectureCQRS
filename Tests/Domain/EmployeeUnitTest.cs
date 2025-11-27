using CleanArchitecture.Domain.Entities.EmployeeAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchitecture.Domain.Exceptions.ContractExceptions;

namespace CleanArchitectureCQRS.Tests.Domain
{
    public class EmployeeUnitTest
    {

        private Employee CreateEmployee()
        {
            var name = "John Doe";
            var email = "jonhdoe@gmail.com";
            var birth = new DateTime(1990, 10, 15);
            var companyId = Guid.NewGuid();

            return Employee.Create(name, email, birth, companyId);
        }

        [Fact]
        public void Employee_Create_ShouldCreateEmployeeSuccessfully()
        {
            var employee = CreateEmployee();

            Assert.NotNull(employee);
        }

        [Fact]
        public void Employee_Create_ShouldThrowException_WhenBirthDateIsInFuture()
        {
            var name = "John Doe";
            var email = "jonhdoe@gmail.com";
            var birth = new DateTime(DateTime.Now.Year + 1, 10, 15);
            var companyId = Guid.NewGuid();

            Assert.Throws<Exception>(() => { Employee.Create(name, email, birth, companyId); });
        }

        [Fact]
        public void Employee_AddContract_ShouldAddContractSuccessfully()
        {
            var employee = CreateEmployee();

            var admissionDate = new DateTime(2025, 5, 1);
            var firstProbationPeriodDays = 45;
            var secondProbationPeriodDays = 45;
            var salary = 4200m;
            var managerId = Guid.NewGuid();

            var contract = employee.AddContract(admissionDate, firstProbationPeriodDays, secondProbationPeriodDays, salary, managerId);

            Assert.NotNull(contract);
            Assert.Contains(contract, employee.Contracts);
            Assert.Equal(contract.FirstProbationEndDate, new DateTime(2025, 06, 15));
            Assert.Equal(contract.SecondProbationEndDate, new DateTime(2025, 07, 30));
        }

        [Fact]
        public void Employee_AddContract_ShouldThrowException_WhenProbationPeriodDaysExceed()
        {
            var employee = CreateEmployee();

            var admissionDate = new DateTime(2025, 5, 1);
            var firstProbationPeriodDays = 50;
            var secondProbationPeriodDays = 45;
            var salary = 4200m;
            var managerId = Guid.NewGuid();

            Assert.Throws<InvalidProbationPeriodException>(() => employee.AddContract(admissionDate, firstProbationPeriodDays, secondProbationPeriodDays, salary, managerId));
        }
    }
}
