using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService_Domain.Entities;

namespace UserService_Infraestructure.Context.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(u => u.UserRoles)
                   .WithOne(ur => ur.User)
                   .HasForeignKey(ur => ur.IdUser);

            //builder.HasMany(u => u.Roles)
            //.WithMany(r => r.Users)
            //.UsingEntity<UserRoles>(
            //    j => j.HasOne(ur => ur.Role)
            //          .WithMany()
            //          .HasForeignKey(ur => ur.IdRole),
            //    j => j.HasOne(ur => ur.User)
            //          .WithMany()
            //          .HasForeignKey(ur => ur.IdUser),
            //    j =>
            //    {
            //        j.HasKey(ur => new { ur.IdUser, ur.IdRole });
            //    });
        }
    }
}
