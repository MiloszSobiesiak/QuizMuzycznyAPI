using System.Collections.Concurrent;
using QuizMuzycznyAPI.Features.Games.Models;

namespace QuizMuzycznyAPI.Repositories.Games;

public class GamesRepository: IGamesRepository
{
    private readonly ConcurrentDictionary<Guid, GameRoom> _games = new();

    public void CreateGame(GameRoom room)
    {
        _games[room.GameId] = room;
    }
    public GameRoom GetGame(Guid gameId)
    {
        _games.TryGetValue(gameId, out var game);
        return game;
    }
}