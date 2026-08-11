using CatalogApi.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokens;
    public AuthController(ITokenService tokens) => _tokens = tokens;

    public record LoginRequest(string Username, string Password);
    public record LoginResponse(string Token);

    // DEMO credentials only. A real app looks the user up and compares a HASHED
    // password from the database — never a hard-coded string like this.
    [HttpPost("login")]
    public ActionResult<LoginResponse> Login(LoginRequest req)
    {
        
        // TODO (you) — Day 6: admin/password → token with role "Admin"; user/password → role "User";
        // anything else → Unauthorized() (401 — bad credentials, not "200 with an empty body").
        if (req.Username == "admin" && req.Password == "password")
        {

            return new LoginResponse(_tokens.CreateToken(req.Username, "admin"));
        }
        else if ((req.Username == "user") && req.Password == "password")
        {
            return new LoginResponse(_tokens.CreateToken(req.Username, "user"));
        }

        return Unauthorized();

    }
}
