using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace UserService_Domain.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Column("name", TypeName = "varchar(100)")]
        [Required]
        public string Name { get; set; }
        [Column("email", TypeName = "varchar(200)")]
        [Required]
        public string Email { get; set; }
        [Column("password", TypeName = "varchar(30)")]
        [Required]
        public string Password { get; set; }
        [Column("cpf", TypeName = "varchar(11)")]
        [Required]
        public string CPF { get; set; }
        [Column("dateofbirth", TypeName = "timestamp with time zone")]
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Column("created", TypeName = "timestamp with time zone")]
        [Required]
        public DateTime Created { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();

        public User()
        {
            
        }

        public User(string name, string email, string password, string cpf, DateTime dateOfBirth)
        {
            ValidateData(name, email, password, cpf, dateOfBirth);

            Name = name.Trim();
            Email = email.Trim();
            Password = password.Trim();
            CPF = cpf.Trim();
            DateOfBirth = dateOfBirth;
            Created = DateTime.UtcNow;
        }

        public void ValidateData(string name, string email, string password, string cpf, DateTime dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 100) throw new Exception("O nome é obrigatório e deve conter no máximo 100 caracteres.");

            if (string.IsNullOrWhiteSpace(email)) throw new Exception("O e-mail é obrigatório.");

            if (!IsValidEmail(email)) throw new Exception("O e-mail informado é inválido.");

            if (email.Length > 200) throw new Exception("O e-mail deve conter no máximo 200 caracteres.");

            if (string.IsNullOrWhiteSpace(password)) throw new Exception("A senha é obrigatória.");

            if (password?.Length < 6 || password?.Length > 30) throw new Exception("A senha deve conter pelo menos 6 caracteres e no máximo 30 caracteres.");

            if (string.IsNullOrWhiteSpace(cpf)) throw new Exception("O CPF é obrigatório.");

            if (dateOfBirth == default) throw new Exception("A data de nascimento é obrigatória.");

            if (dateOfBirth > DateTime.UtcNow.AddYears(-18)) throw new Exception("O usuário deve ter pelo menos 12 anos.");
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }
    }
}
