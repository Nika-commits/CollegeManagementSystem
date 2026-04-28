using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CollegeManagementSystem.Services;

public class JwtService
{

    public string GenerateToken()
    {
        var secretKey = "wsdsdf-sdfsdfv-sdfsd-sdfsdfsdf";

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenObj = new JwtSecurityToken
        (
            "http://localhost:5148",
            "http://localhost:3000",
            [],
            signingCredentials: signingCredential,
            expires: DateTime.UtcNow.AddHours(2)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenObj);
        return token;
    }
}