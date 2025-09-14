namespace QuizMuzycznyAPI.Features.Users.Models;

public class User
{
    public int Id { get; set; }
    public string SpotifyId { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastLogin { get; set; }
    public string SpotifyAccessToken { get; set; }
    public string SpotifyRefreshToken { get; set; }
    public DateTime SpotifyTokenExpiry { get; set; }
}