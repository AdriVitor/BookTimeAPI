using ResourceService_Domain.Entities;

namespace ResourceService.UnitTests.Domain
{
    public class ResourceTests
    {
        private Resource CreateValidResource()
        {
            return new Resource(
                idUser: 1,
                name: "Recurso v�lido",
                description: "Descri��o v�lida com mais de 10 caracteres",
                idUf: 1,
                address: "Endere�o v�lido"
            );
        }

        [Fact]
        public void CreateResource_ShouldCreateWithValidData()
        {
            // Act
            var resource = CreateValidResource();

            // Assert
            Assert.Equal(1, resource.IdUser);
            Assert.Equal("Recurso v�lido", resource.Name);
            Assert.Equal("Descri��o v�lida com mais de 10 caracteres", resource.Description);
            Assert.Equal(1, resource.IdUf);
            Assert.Equal("Endere�o v�lido", resource.Address);
            Assert.True(resource.CreatedAt <= DateTime.UtcNow);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ValidateData_ShouldThrow_WhenIdUserIsInvalid(int invalidIdUser)
        {
            // Act
            Action action = () => new Resource(
                invalidIdUser,
                "Nome",
                "Descri��o v�lida 123",
                1,
                "Endere�o"
            );

            Assert.Throws<Exception>(action);
        }

        [Fact]
        public void ValidateData_ShouldThrow_WhenNameIsEmpty()
        {
            Action action = () => new Resource(
                1,
                "",
                "Descri��o v�lida 123",
                1,
                "Endere�o"
            );

            Assert.Throws<Exception>(action);
        }

        [Fact]
        public void ValidateData_ShouldThrow_WhenNameIsTooLong()
        {
            var longName = new string('A', 71);

            Action action = () => new Resource(
                1,
                longName,
                "Descri��o v�lida 123",
                1,
                "Endere�o"
            );

            Assert.Throws<Exception>(action);
        }

        [Fact]
        public void ValidateData_ShouldThrow_WhenDescriptionIsEmpty()
        {
            Action action = () => new Resource(
                1,
                "Nome",
                "",
                1,
                "Endere�o"
            );

            Assert.Throws<Exception>(action);
        }

        [Fact]
        public void ValidateData_ShouldThrow_WhenDescriptionIsShort()
        {
            Action action = () => new Resource(
                1,
                "Nome",
                "12345",
                1,
                "Endere�o"
            );

            Assert.Throws<Exception>(action);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void ValidateData_ShouldThrow_WhenIdUfIsInvalid(int invalidUf)
        {
            Action action = () => new Resource(
                1,
                "Nome",
                "Descri��o v�lida 123",
                invalidUf,
                "Endere�o"
            );

            Assert.Throws<Exception>(action);
        }

        [Fact]
        public void ValidateData_ShouldThrow_WhenAddressIsEmpty()
        {
            Action action = () => new Resource(
                1,
                "Nome",
                "Descri��o v�lida 123",
                1,
                ""
            );

            Assert.Throws<Exception>(action);
        }

        [Fact]
        public void ValidateData_ShouldThrow_WhenAddressIsTooLong()
        {
            var longAddress = new string('A', 151);

            Action action = () => new Resource(
                1,
                "Nome",
                "Descri��o v�lida 123",
                1,
                longAddress
            );

            Assert.Throws<Exception>(action);
        }

        [Fact]
        public void CreateResource_ShouldTrimStrings()
        {
            var resource = new Resource(
                1,
                "   Nome   ",
                "   Descri��o v�lida 123   ",
                1,
                "   Endere�o   "
            );

            Assert.Equal("Nome", resource.Name);
            Assert.Equal("Descri��o v�lida 123", resource.Description);
            Assert.Equal("Endere�o", resource.Address);
        }
    }
}
