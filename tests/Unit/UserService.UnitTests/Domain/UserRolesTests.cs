using NuGet.Frameworks;
using UserService_Domain.Entities;

namespace UserService.UnitTests.Domain
{
    public class UserRolesTests
    {
        [Fact]
        public void ValidateIfIdUserExists()
        {
            var act = () => new UserRoles(0, 10);

            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("Preencha todos os IDs para continuar", exception.Message);
        }

        [Fact]
        public void ValidateIfIdRoleExists()
        {
            var act = () => new UserRoles(10, 0);

            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("Preencha todos os IDs para continuar", exception.Message);
        }
    }
}
