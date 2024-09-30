using Microsoft.AspNetCore.Mvc;
/*1.1. The student needs to implement a login system for an admin user. This will lay the basic fundament for authorization to our application. 
•	The login system will consist of a POST call that receives a username and password and checks if the password is correct with what is in the database. 
This endpoint will also register a session on the server. 
•	The POST endpoint should return a success message if the password is correct, else it should return reasonable feedback of what went wrong. 
•	Create a GET endpoint that returns a Boolean value based on if the session is registered or not. Additionally, return the name of the admin user that is logged in. 
•	The login logic and endpoints need to be separated in a Service and a Controller.
*/



[Route("")]
public class LogInController : Controller
{

    private readonly ILoginService _loginService;

    public LogInController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("LogIn")]
    public async Task<IActionResult> LogInAttempt([FromBody] Account account)
    {
        if (_loginService.CheckSession()) // Q does this work when multiple are logged in?
        {
            return Ok($"you are already logged in user {_loginService.GetLoggedInUser().Username}");
        }
        var result = await _loginService.LogInAsync(account);
        if (result)
        {
            return Ok("Account found");
        }

        return BadRequest("Invalid username and/or password");



    }

    [HttpGet("CheckSession")]
    public async Task<bool> CheckSession()
    {
        // if (_loginService.CheckSession())
        // {
        //     return _loginService.CheckSession();
        // }
        return _loginService.CheckSession();
    }

    [HttpGet("CheckUser")]
    public IActionResult CheckUser()
    {
        var user = _loginService.GetLoggedInUser();
        if (user != null)
        {
            return Ok(user);
        }

        return BadRequest("No user found");
    }

    [HttpGet("LogOut")]
    public IActionResult LogOut()
    {
        _loginService.LogOut();
        return Ok("Logged out successfully");
    }


}



