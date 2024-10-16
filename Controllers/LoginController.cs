using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
/*1.1. The student needs to implement a login system for an admin user. This will lay the basic fundament for authorization to our application. 
•	The login system will consist of a POST call that receives a username and password and checks if the password is correct with what is in the database. 
This endpoint will also register a session on the server. 
•	The POST endpoint should return a success message if the password is correct, else it should return reasonable feedback of what went wrong. 
•	Create a GET endpoint that returns a Boolean value based on if the session is registered or not. Additionally, return the name of the admin user that is logged in. 
•	The login logic and endpoints need to be separated in a Service and a Controller.
*/





[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILoginService _loginService;

    public AuthController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User user)
    {
    
        if (_loginService.Login(user))
        {
            HttpContext.Session.SetString("Username", user.Email);
            return Ok("Login successful.");
        }
        return Unauthorized("Invalid username or password.");
    }
    [HttpPost("Register")]
    public IActionResult Register([FromBody] User user)
    {
        if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
        {
            return BadRequest("Email and Password are required.");
        }

        bool check = _loginService.Register(user);
        if (check)
        {
            return Ok(user.First_Name +" " +user.Last_Name + " has been registered");
        }
        
        return BadRequest("Registration failed");
    }



    [HttpGet("CheckSession")]
    public IActionResult CheckSession()
    {
        var username = HttpContext.Session.GetString("Username");
        if (username != null)
        {
            return Ok(new { IsLoggedIn = true, CurrentUser = username });
        }
        return Ok(new { IsLoggedIn = false });
    }

    [HttpGet("CheckUser")]
    public IActionResult CheckUser()
    {
        var username = HttpContext.Session.GetString("Username");
        return Ok(new { Username = username });
    }

    [HttpGet("LogOut")]
    public IActionResult LogOut()
    {
        HttpContext.Session.Remove("Username"); // Clear session
        return Ok(new { Message = "Logged out successfully" });
    }
}

