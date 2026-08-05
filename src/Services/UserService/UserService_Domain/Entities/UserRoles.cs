using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UserService_Domain.Entities
{
    [Table("role_user")]
    public class UserRoles
    {
        [Column("userid", TypeName = "int")]
        [Required]
        public int IdUser { get; set; }
        [Column("roleid", TypeName = "int")]
        [Required]
        public int IdRole { get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!;
        [JsonIgnore]
        public Role Role { get; set; } = null!;

        public UserRoles(int idUser, int idRole)
        {
            ValidateData(idUser, idRole);

            IdUser = idUser;
            IdRole = idRole;
        }

        public void ValidateData(int idUser, int idRole)
        {
            if (idUser == 0 || idRole == 0) throw new Exception("Preencha todos os IDs para continuar");
        }
    }
}
