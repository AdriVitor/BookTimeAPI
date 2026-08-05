using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService_Domain.Entities;

namespace UserService_Infraestructure.Context.Config
{
    public class RolesConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(r => r.UserRoles)
                   .WithOne(ur => ur.Role)
                   .HasForeignKey(ur => ur.IdRole);

            //builder.HasMany(u => u.Users)
            //.WithMany(r => r.Roles)
            //.UsingEntity<UserRoles>(
            //    j => j.HasOne(ur => ur.User)
            //          .WithMany()
            //          .HasForeignKey(ur => ur.IdUser),
            //    j => j.HasOne(ur => ur.Role)
            //          .WithMany()
            //          .HasForeignKey(ur => ur.IdRole),
            //    j =>
            //    {
            //        j.HasKey(ur => new { ur.IdUser, ur.IdRole });
            //    });
        }
    }
}
