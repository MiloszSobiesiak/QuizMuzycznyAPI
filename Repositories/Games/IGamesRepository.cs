using QuizMuzycznyAPI.Features.Games.Models;

namespace QuizMuzycznyAPI.Repositories.Games;

public interface IGamesRepository
{
    void CreateGame(GameRoom room);
    GameRoom GetGame(Guid gameId);
}