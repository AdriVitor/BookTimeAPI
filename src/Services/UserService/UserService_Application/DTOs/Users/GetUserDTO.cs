using UserService_Domain.Entities;

namespace UserService_Application.DTOs.User
{
    public class GetUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }

        public GetUserDTO ConvertToDTO(UserService_Domain.Entities.User users)
        {
            return new GetUserDTO()
            {
                Id = users.Id,
                Name = users.Name,
                Email = users.Email,
                Password = users.Password,
                CPF = users.CPF,
                DateOfBirth = users.DateOfBirth,
                Created = users.Created
            };
        }

    }
}
