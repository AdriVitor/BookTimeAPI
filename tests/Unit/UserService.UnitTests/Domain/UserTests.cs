using UserService_Domain.Entities;

namespace UserService.UnitTests.Domain
{
    public class UserDomainTests
    {
        [Fact]
        public void Constructor_ShouldThrow_WhenNameIsEmpty()
        {
            var act = () => new User("", "email@test.com", "123456", "12345678901", DateTime.UtcNow.AddYears(-20));

            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("O nome é obrigatório e deve conter no máximo 100 caracteres.", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenNameIsTooLong()
        {
            var longName = new string('A', 101);

            var act = () => new User(longName, "email@test.com", "123456", "12345678901", DateTime.UtcNow.AddYears(-20));

            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("O nome é obrigatório e deve conter no máximo 100 caracteres.", exception.Message);
        }


        [Fact]
        public void Constructor_ShouldThrow_WhenEmailIsEmpty()
        {
            var act = () => new User("John Doe", "", "123456", "12345678901", DateTime.UtcNow.AddYears(-20));

            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("O e-mail é obrigatório.", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenEmailIsInvalid()
        {
            var act = () => new User("John Doe", "invalid_email", "123456", "12345678901", DateTime.UtcNow.AddYears(-20));

            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("O e-mail informado é inválido.", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenEmailTooLong()
        {
            var longEmail = new string('a', 201) + "@test.com";

            var act = () => new User("John Doe", longEmail, "123456", "12345678901", DateTime.UtcNow.AddYears(-20));

            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("O e-mail deve conter no máximo 200 caracteres.", exception.Message);
        }


        [Fact]
        public void Constructor_ShouldThrow_WhenPasswordIsEmpty()
        {
            var act = () => new User("John Doe", "email@test.com", "", "12345678901", DateTime.UtcNow.AddYears(-20));

            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("A senha é obrigatória.", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenPasswordTooShort()
        {
            var act = () => new User("John Doe", "email@test.com", "123", "12345678901", DateTime.UtcNow.AddYears(-20));

            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("A senha deve conter pelo menos 6 caracteres e no máximo 30 caracteres.", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenPasswordTooLong()
        {
            var longPassword = new string('A', 31);

            var act = () => new User("John Doe", "email@test.com", longPassword, "12345678901", DateTime.UtcNow.AddYears(-20));

            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("A senha deve conter pelo menos 6 caracteres e no máximo 30 caracteres.", exception.Message);
        }


        [Fact]
        public void Constructor_ShouldThrow_WhenCPFIsEmpty()
        {
            var act = () => new User("John Doe", "email@test.com", "123456", "", DateTime.UtcNow.AddYears(-20));

            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("O CPF é obrigatório.", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenDateOfBirthIsDefault()
        {
            var act = () => new User("John Doe", "email@test.com", "123456", "12345678901", default);

            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("A data de nascimento é obrigatória.", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenUserIsYoungerThan12()
        {
            var tooYoung = DateTime.UtcNow.AddYears(-10);

            var act = () => new User("John Doe", "email@test.com", "123456", "12345678901", tooYoung);

            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("O usuário deve ter pelo menos 12 anos.", exception.Message);
        }

        [Fact]
        public void Constructor_ShouldCreateUser_WhenDataIsValid()
        {
            // Arrange
            var birth = DateTime.UtcNow.AddYears(-25);

            // Act
            var user = new User("John Doe", "email@test.com", "123456", "12345678901", birth);

            // Assert
            Assert.Equal("John Doe", user.Name);
            Assert.Equal("email@test.com", user.Email);
            Assert.Equal("123456", user.Password);
            Assert.Equal("12345678901", user.CPF);
            Assert.Equal(birth, user.DateOfBirth);
            Assert.True(user.Created <= DateTime.UtcNow);
        }
    }
}
