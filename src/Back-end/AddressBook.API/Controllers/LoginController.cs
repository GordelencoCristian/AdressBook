using AddressBook.DataTrasnferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AddressBook.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult Login(LoginDto loginDto)
    {
        try
        {
            if (string.IsNullOrEmpty(loginDto.UserName) ||
                string.IsNullOrEmpty(loginDto.Password))
                return BadRequest("Username and/or Password not specified");

            if (loginDto.UserName.Equals("Gordelenco") &&
                loginDto.Password.Equals("Cristian"))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisisaverylongsecretkey@1234567890"));

                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    "ADDRESSBOOK",
                    "http://localhost:51398",
                    new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: signinCredentials
                );

                return Ok(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
            }
        }
        catch
        {
            return BadRequest("An error occurred in generating the token");
        }

        return Unauthorized();
    }
}