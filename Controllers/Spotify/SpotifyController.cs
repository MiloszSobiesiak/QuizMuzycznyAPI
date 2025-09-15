using System.Text;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizMuzycznyAPI.Features.Sessions.Commands.CreateSession;
using QuizMuzycznyAPI.Features.Sessions.Queries.GetSessionUser;
using QuizMuzycznyAPI.Features.Spotify.Models;
using QuizMuzycznyAPI.Features.Spotify.Queries.GetFavourites;
using QuizMuzycznyAPI.Features.Users.Commands.CreateOrUpdateUser;
using QuizMuzycznyAPI.Features.Users.Models;

[ApiController]
[Route("api/spotify")]
public class SpotifyController(IMediator mediator, IHttpClientFactory httpClientFactory, IConfiguration config)
    : ControllerBase
{
    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code)
    {
        var clientId = config["Spotify:ClientId"];
        var clientSecret = config["Spotify:ClientSecret"];
        var redirectUri = "https://empathetic-youth-production.up.railway.app/api/spotify/callback";
        
        var client = httpClientFactory.CreateClient();
        var body = new Dictionary<string, string>
        {
            { "grant_type", "authorization_code" },
            { "code", code },
            { "redirect_uri", redirectUri }
        };
        var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token")
        {
            Content = new FormUrlEncodedContent(body)
        };
        var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeader);
        
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        var tokenJson = await response.Content.ReadAsStringAsync();
        var tokenData = System.Text.Json.JsonSerializer.Deserialize<SpotifyTokenResponse>(tokenJson);

        // 2. Pobranie danych użytkownika
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenData.access_token);
        var userResponse = await client.GetAsync("https://api.spotify.com/v1/me");
        var userJson = await userResponse.Content.ReadAsStringAsync();
        var spotifyUser = System.Text.Json.JsonSerializer.Deserialize<SpotifyUserDTO>(userJson);

        // Przypisanie tokenów
        spotifyUser.AccessToken = tokenData.access_token;
        spotifyUser.RefreshToken = tokenData.refresh_token;
        spotifyUser.TokenExpiry = DateTime.UtcNow.AddSeconds(tokenData.expires_in);

        // 3. Tworzenie/aktualizacja użytkownika
        var appUser = await mediator.Send(new CreateOrUpdateUserCommand(spotifyUser));

        // 4. Tworzenie sesji
        var sessionId = Guid.NewGuid().ToString();
        await mediator.Send(new CreateSessionCommand(sessionId, appUser));

        Response.Cookies.Append("session_id", sessionId, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.Now.AddDays(7)
        });

        return Redirect("https://quiz-muzyczny.vercel.app/home-page");
    }
    
    [HttpGet("playlists")]
    public async Task<IActionResult> GetPlaylists()
    {
        if (!Request.Cookies.TryGetValue("session_id", out var sessionId))
            return Unauthorized();

        var user = await mediator.Send(new GetSessionUserQuery(sessionId));
        if (user == null) return Unauthorized();

        var client = httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.SpotifyAccessToken);
        var response = await client.GetAsync("https://api.spotify.com/v1/me/playlists");
        var json = await response.Content.ReadAsStringAsync();

        return Content(json, "application/json");
    }
    
    [HttpGet("favourites")]
    public async Task<IActionResult> GetFavourites()
    {
        var result = await mediator.Send(new GetFavouritesQuery());
        return Ok(result);
    }
}
