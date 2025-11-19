using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CleanArchitecture.Infrastructure.Configurations
{
    internal class ContractConfigurations : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.Property(p => p.AdmissionDate)
                .HasColumnType("date");

            builder.Property(p => p.FirstProbationEndDate)
                .HasColumnType("date");

            builder.Property(p => p.SecondProbationEndDate)
                .HasColumnType("date");

            builder.Property(p => p.ApprovedFirstProbationPeriod)
                .IsRequired();

            builder.Property(p => p.ApprovedSecondProbationPeriod)
                .IsRequired();

            builder.Property(p => p.Salary)
                .IsRequired();

            builder.HasOne<Employee>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(p => p.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contract_Employee_ManagerId");


        }
    }
}
