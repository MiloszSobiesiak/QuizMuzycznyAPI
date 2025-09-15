using MediatR;
using QuizMuzycznyAPI.Features.Spotify.Models;
using QuizMuzycznyAPI.Features.Users.Models;
using SpotifyAPI.Web;

namespace QuizMuzycznyAPI.Features.Spotify.Queries.GetFavourites;

public class GetFavouritesQueryHandler(
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<GetFavouritesQuery, IEnumerable<TrackDTO>>
{
    public async Task<IEnumerable<TrackDTO>> Handle(GetFavouritesQuery request, CancellationToken cancellationToken)
    {
        var context = httpContextAccessor.HttpContext;
        var user = (User)context.Items["SpotifyUser"];

        // Tworzymy klienta Spotify z access tokenem
        var spotify = new SpotifyClient(user.SpotifyAccessToken);

        // Pobieramy maksymalnie 50 ulubionych utworów
        var savedTracks = await spotify.Library.GetTracks(new LibraryTracksRequest
        {
            Limit = 50
        });

        // Mapowanie na DTO
        return savedTracks.Items
            .Where(x => x.Track.PreviewUrl != null)
            .Select(x => new TrackDTO
            {
                Id = x.Track.Id,
                Name = x.Track.Name,
                Artist = string.Join(", ", x.Track.Artists.Select(a => a.Name)),
                Album = x.Track.Album.Name,
                Image = x.Track.Album.Images.FirstOrDefault()?.Url,
                PreviewUrl = x.Track.PreviewUrl
            })
            .ToList();
    }
}