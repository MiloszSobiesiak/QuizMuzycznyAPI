namespace QuizMuzycznyAPI.Features.Spotify.Models;

public class TrackDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string Image { get; set; }
    public string PreviewUrl { get; set; }
}