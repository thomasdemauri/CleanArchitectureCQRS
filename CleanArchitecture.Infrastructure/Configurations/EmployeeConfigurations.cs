using CleanArchitecture.Domain.Entities.CompanyAggregate;
using CleanArchitecture.Domain.Entities.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CleanArchitecture.Infrastructure.Configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(P => P.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Birth)
                .IsRequired()
                .HasColumnType("date");

            builder.HasOne<Company>()
                .WithMany()
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(p => p.CurrentContract);

            builder.HasMany(e => e.Contracts)
                .WithOne()
                .HasForeignKey("EmployeeId")
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Contract_Employee_EmployeeId");

        }
    }
}
