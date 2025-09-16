using Microsoft.AspNetCore.SignalR;
using QuizMuzycznyAPI.Repositories.Games;

namespace QuizMuzycznyAPI.Hubs;

public class GamesHub : Hub
{
    private readonly IGamesRepository _repo;

    public GamesHub(IGamesRepository repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }
    
    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"Connected: {Context.ConnectionId}");
        return base.OnConnectedAsync();
    }

    public async Task JoinGame(string gameIdString, string playerName)
    {
        if (!Guid.TryParse(gameIdString, out var gameId))
        {
            Console.WriteLine($"[ERROR] Invalid gameId: {gameIdString}");
            await Clients.Caller.SendAsync("Error", "Invalid game ID");
            return;
        }

        var game = _repo.GetGame(gameId);
        if (game == null)
        {
            Console.WriteLine($"[WARNING] Game {gameId} not found for player {playerName}");
            await Clients.Caller.SendAsync("Error", "Game not found");
            return;
        }

        game.Players.Add(playerName);
        Console.WriteLine($"[INFO] Player {playerName} joined game {gameId}. Total players: {game.Players.Count}");

        await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
        await Clients.Group(gameId.ToString()).SendAsync("PlayerJoined", playerName);
    }
}