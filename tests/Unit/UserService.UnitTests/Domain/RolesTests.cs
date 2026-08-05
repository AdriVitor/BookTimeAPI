using UserService_Domain.Entities;

namespace UserService.UnitTests.Domain
{
    public class RolesTests
    {
        [Fact]
        public void ValidateIfNameIsFilled()
        {
            var actNameNull = () => new Role(null);
            var actNameContainsLessThan3Caracters = () => new Role("ab");

            var exceptionNull = Assert.Throws<Exception>(actNameNull);
            var exceptionContainsLessThan3Caracters = Assert.Throws<Exception>(actNameContainsLessThan3Caracters);

            Assert.Equal(exceptionNull.Message, "O nome deve conter no mínimo 3 caracteres");
            Assert.Equal(exceptionContainsLessThan3Caracters.Message, "O nome deve conter no mínimo 3 caracteres");
        }
    }
}
