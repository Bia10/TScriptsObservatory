using System.Net;
using BlazoriseApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazoriseApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService accountService) : ControllerBase
{
    [HttpGet("getCredentials")]
    [Produces("text/plain")]
    public async Task<IActionResult> GetCredentials()
        => throw new NotImplementedException();

    [HttpGet("getUsername")]
    [Produces("text/plain")]
    public async Task<IActionResult> GetUsername()
    {
        try
        {
            if (Request.Path != "/api/Account/getUsername" ||
                Request.Method != "GET" ||
                Request.Headers.UserAgent != "TScripts/1.0" ||
                Request.ContentType != "application/x-www-form-urlencoded")
                return BadRequest("Invalid request.");

            var accountUsername = await accountService.GetUsernameAsync().ConfigureAwait(false);
            if (string.IsNullOrEmpty(accountUsername))
                return NotFound();

            return Ok(accountUsername);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An exception occurred: {ex.Message}");
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing the request.");
        }
    }

    [HttpGet("getPassword")]
    [Produces("text/plain")]
    public async Task<IActionResult> GetPassword()
    {
        try
        {
            if (Request.Path != "/api/Account/getPassword" ||
                Request.Method != "GET" ||
                Request.Headers.UserAgent != "TScripts/1.0" ||
                Request.ContentType != "application/x-www-form-urlencoded")
                return BadRequest("Invalid request.");

            var accountPassword = await accountService.GetPasswordAsync().ConfigureAwait(false);
            if (string.IsNullOrEmpty(accountPassword))
                return NotFound();

            return Ok(accountPassword);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An exception occurred: {ex.Message}");
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing the request.");
        }
    }
}