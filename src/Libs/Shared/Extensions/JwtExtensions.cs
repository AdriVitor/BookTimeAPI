using System.IdentityModel.Tokens.Jwt;

namespace Shared.Extensions
{
    public static class JwtExtensions
    {
        public static int GetCustomerId(this string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(token.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase));

            var claim = jwt.Claims.First(c => c.Type == "sub");

            return int.Parse(claim.Value);
        }
    }
}
