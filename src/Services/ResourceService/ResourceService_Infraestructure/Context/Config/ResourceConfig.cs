using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceService_Domain.Entities;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable("resource");

        builder.HasKey(r => r.Id).HasName("id");

        builder.HasOne(r => r.Uf)
               .WithMany(u => u.Resources)
               .HasForeignKey(r => r.IdUf)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
