using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizMuzycznyAPI.Features.Games.Commands.CreateGame;
using QuizMuzycznyAPI.Features.Games.Models;

namespace QuizMuzycznyAPI.Controllers.Games;

[ApiController]
[Route("api/[controller]")]
public class GameController(IMediator mediator) : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult<GameCreatedResponse>> Create([FromBody] GameConfig request)
    {
        var result = await mediator.Send(new CreateGameCommand(request));
        return Ok(result);
    }
}