namespace QuizMuzycznyAPI.Features.Games.Models;

public class GameRoom
{
    public Guid GameId { get; set; }
    public GameConfig Config { get; set; } = new();
    public List<string> Players { get; set; } = new();
}