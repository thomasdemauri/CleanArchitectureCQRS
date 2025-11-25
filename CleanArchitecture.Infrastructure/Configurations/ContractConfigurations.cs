using CleanArchitecture.Domain.Entities.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CleanArchitecture.Infrastructure.Configurations
{
    internal class ContractConfigurations : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Property(p => p.AdmissionDate)
                .HasColumnType("date");

            builder.Property(p => p.FirstProbationEndDate)
                .HasColumnType("date");

            builder.Property(p => p.SecondProbationEndDate)
                .HasColumnType("date");

            builder.Property(p => p.ApprovedFirstProbationPeriod)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(p => p.ApprovedSecondProbationPeriod)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(p => p.Salary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property<Guid>("EmployeeId")
            .IsRequired();

            builder.HasOne<Employee>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(c => c.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contract_Employee_ManagerId");

        }
    }
}
