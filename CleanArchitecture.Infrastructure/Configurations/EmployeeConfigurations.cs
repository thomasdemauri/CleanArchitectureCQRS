using CleanArchitecture.Domain.Entities;
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

            builder.Property(p => p.CompanyId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");
        }
    }
}
