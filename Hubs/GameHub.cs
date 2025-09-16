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
        var game = repo.GetGame(gameId);
        if (game == null)
        {
            await Clients.Caller.SendAsync("Error", "Game not found");
            return;
        }

        game.Players.Add(playerName);
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
        
        Console.WriteLine($"[INFO] Player {playerName} joined game {gameId}. Total players: {game.Players.Count}");

        await Clients.Group(gameId.ToString()).SendAsync("PlayerJoined", playerName);
    }
}