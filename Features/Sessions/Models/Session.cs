using QuizMuzycznyAPI.Features.Users.Models;

namespace QuizMuzycznyAPI.Features.Sessions.Models;

public class Session
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string SessionId { get; set; } = null!; 
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public User User { get; set; } = null!;
}