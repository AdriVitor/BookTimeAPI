using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Xml.Linq;

namespace ResourceService_Domain.Entities
{
    [Table("resource")]
    public class Resource
    {
        [Key]
        [Column("id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Column("iduser", TypeName = "int")]
        [Required]
        public int IdUser { get; set; }
        [Column("name", TypeName = "varchar(70)")]
        [Required]
        public string Name { get; set; }
        [Column("description", TypeName = "varchar(max)")]
        [Required]
        public string Description { get; set; }
        [Column("iduf", TypeName = "int")]
        [Required]
        public int IdUf { get; set; }
        [Column("address", TypeName = "varchar(150)")]
        [Required]
        public string Address { get; set; }
        [Column("created_at", TypeName = "TIMESTAMP with time zone")]
        [Required]
        public DateTime CreatedAt { get; set; }

        public Uf Uf { get; set; }

        public Resource()
        {
            
        }

        public Resource(int idUser, string name, string description, int idUf, string address)
        {
            ValidateData(idUser, name, description, idUf, address);

            IdUser = idUser;
            Name = name.Trim();
            Description = description.Trim();
            IdUf = idUf;
            Address = address.Trim();
            CreatedAt = DateTime.UtcNow;
        }

        public void ValidateData(int idUser, string name, string description, int idUf, string address)
        {
            if (idUser <= 0) throw new Exception("O ID do usuário proprietário é obrigatório e deve ser válido.");

            if (string.IsNullOrWhiteSpace(name) || name?.Length > 70) throw new Exception("O nome do recurso é obrigatório e deve conter no máximo 70 caracteres.");

            if (string.IsNullOrWhiteSpace(description) || description?.Length < 10) throw new Exception("A descrição do recurso é obrigatória e deve conter pelo menos 10 caracteres.");

            if (idUf <= 0) throw new Exception("O ID da UF é obrigatório e deve ser válido.");

            if (string.IsNullOrWhiteSpace(address) || address?.Length > 150) throw new Exception("O endereço do recurso é obrigatório e deve conter no máximo 150 caracteres.");
        }
    }
}
