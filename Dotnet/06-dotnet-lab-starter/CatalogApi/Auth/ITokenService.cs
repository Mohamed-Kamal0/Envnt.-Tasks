namespace CatalogApi.Auth;

// Turns a verified user into a signed JWT.
public interface ITokenService
{
    string CreateToken(string username, string role);
}
