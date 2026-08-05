namespace UserService_Application.DTOs.Users
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public int IdRole { get; set; }

        public UserService_Domain.Entities.User ConvertToEntity()
        {
            return new UserService_Domain.Entities.User(Name, Email, Password, CPF, DateOfBirth);
        }
    }
}
