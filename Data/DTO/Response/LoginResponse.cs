namespace CollegeManagementSystem.Data.DTO.Response;

public class LoginResponse
{
    public bool Success { get; set; }

    public string? Error { get; set; }

    public string? Token { get; set; }

    public string? RefreshToken { get; set; }

    public static LoginResponse Fail(string error)
    {
        return new LoginResponse
        {
            Success = false,
            Error = error
        };
    }

    public static LoginResponse Ok(string token)
    {
        return new LoginResponse
        {
            Success = true,
            Token = token
        };
    }
}