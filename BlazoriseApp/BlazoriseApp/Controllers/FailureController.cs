using System.Net;
using System.Text;
using BlazoriseApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BlazoriseApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FailureController : ControllerBase
{
    [HttpPost("postReport")]
    public async Task<IActionResult> PostFailureReport()
    {
        try
        {
            if (Request.Path != "/api/Failure/postReport"
                || Request.Method != "POST"
                || Request.Headers.UserAgent != "TScripts/1.0"
                || Request.ContentType != "application/x-www-form-urlencoded")
                return BadRequest("Invalid request.");

            string msgData;
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                msgData = await reader.ReadToEndAsync().ConfigureAwait(false);
            }

            if (string.IsNullOrWhiteSpace(msgData))
                return BadRequest("Message data is empty.");

            var report = FailureReportDTO.Parse(msgData);
            Console.WriteLine(report);

            return Ok("Failure report received successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An exception occurred: {ex.Message}");
            return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing the request.");
        }
    }
}