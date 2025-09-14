using System.Text.Json.Serialization;

namespace QuizMuzycznyAPI.Features.Users.Models;

public class SpotifyUserDTO
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }

    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime TokenExpiry { get; set; }
}