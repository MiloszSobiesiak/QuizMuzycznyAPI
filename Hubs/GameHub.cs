using Microsoft.AspNetCore.SignalR;
using QuizMuzycznyAPI.Repositories.Games;

namespace QuizMuzycznyAPI.Hubs;

public class GamesHub(IGamesRepository repo) : Hub
{
    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"Connected: {Context.ConnectionId}");
        return base.OnConnectedAsync();
    }

    public async Task JoinGame(Guid gameId, string playerName)
    {
        Console.WriteLine($"Connected: {gameId}");
        
        var game = repo.GetGame(gameId);
        Console.WriteLine($"Connected: {game.GameId}");

        game.Players.Add(playerName);
        
        Console.WriteLine($"[INFO] Player {playerName} joined game {gameId}. Total players: {game.Players.Count}");

        await Clients.Group(gameId.ToString()).SendAsync("PlayerJoined", playerName);
    }
}