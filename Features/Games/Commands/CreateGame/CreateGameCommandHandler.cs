using MediatR;
using QuizMuzycznyAPI.Features.Games.Models;
using QuizMuzycznyAPI.Repositories.Games;

namespace QuizMuzycznyAPI.Features.Games.Commands.CreateGame;

public class CreateGameCommandHandler(IGamesRepository repo) : IRequestHandler<CreateGameCommand, GameCreatedResponse>
{
    public Task<GameCreatedResponse> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var room = new GameRoom
        {
            GameId = id,
            Config = request.Config,
        };
        repo.CreateGame(room);
        return Task.FromResult(new GameCreatedResponse
        {
            GameId = id
        });
    }
}