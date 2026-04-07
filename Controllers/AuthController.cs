using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost]
    public async Task<string> RegisterUser()
    {
        return "0";
    }
}