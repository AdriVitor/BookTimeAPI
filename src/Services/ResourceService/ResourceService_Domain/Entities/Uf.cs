using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceService_Domain.Entities
{
    [Table("uf")]
    public class Uf
    {
        [Key]
        [Column("id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Column("name", TypeName = "varchar(100)")]
        [Required]
        public string Name { get; set; }
        [Column("acronym", TypeName = "char(2)")]
        [Required]
        public string Acronym { get; set; }

        public ICollection<Resource> Resources { get; set; }
    }
}
