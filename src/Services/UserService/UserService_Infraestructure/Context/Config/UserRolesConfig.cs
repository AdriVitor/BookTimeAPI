using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService_Domain.Entities;

namespace UserService_Infraestructure.Context.Config
{
    public class UserRolesConfig : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {
            builder.HasKey(x => new { x.IdRole, x.IdUser });

            builder.Property(x => x.IdRole).HasColumnName("roleid");

            builder.Property(x => x.IdUser).HasColumnName("userid");

            builder.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.IdUser)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.IdRole)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
