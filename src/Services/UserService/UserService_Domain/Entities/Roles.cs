using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService_Domain.Entities
{
    [Table("roles")]
    public class Role
    {
        [Key]
        [Column("id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Column("name", TypeName = "varchar(35)")]
        [Required]
        public string Name { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();

        public Role()
        {
            
        }

        public Role(string name)
        {
            ValidateData(name);

            Name = name;
        }

        public void ValidateData(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name?.Length < 3)
                throw new Exception("O nome deve conter no mínimo 3 caracteres");
        }
    }
}
