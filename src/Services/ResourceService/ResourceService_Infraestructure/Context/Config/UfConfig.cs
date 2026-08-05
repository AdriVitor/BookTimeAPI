using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceService_Domain.Entities;

public class UfConfiguration : IEntityTypeConfiguration<Uf>
{
    public void Configure(EntityTypeBuilder<Uf> builder)
    {
        builder.ToTable("uf");

        builder.HasKey(u => u.Id);

        builder.HasMany(u => u.Resources)
               .WithOne(r => r.Uf)
               .HasForeignKey(r => r.IdUf)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
