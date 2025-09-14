using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizMuzycznyAPI.Features.Sessions.Commands.DeleteSession;
using QuizMuzycznyAPI.Features.Sessions.Queries.GetSessionUser;

namespace QuizMuzycznyAPI.Controllers.Sessions;

[ApiController]
[Route("api/sessions")]
public class SessionsController(IMediator mediator) : ControllerBase
{
    [HttpGet("is-logged-in")]
    public async Task<IActionResult> IsLoggedIn()
    {
        if (!Request.Cookies.TryGetValue("session_id", out var sessionId))
            return Unauthorized();

        var user = await mediator.Send(new GetSessionUserQuery(sessionId));
        if (user == null) return Unauthorized();

        return Ok(new
        {
            user.DisplayName,
            user.Email,
        });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        if (!Request.Cookies.TryGetValue("session_id", out var sessionId))
            return BadRequest("Brak sesji do wylogowania");
    
        await mediator.Send(new DeleteSessionCommand(sessionId));
        Response.Cookies.Delete("session_id");
    
        return Ok();
    }
}