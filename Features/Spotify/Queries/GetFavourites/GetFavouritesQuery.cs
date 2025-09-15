using MediatR;
using QuizMuzycznyAPI.Features._Base.RequireSpotifyAuth;
using QuizMuzycznyAPI.Features.Spotify.Models;

namespace QuizMuzycznyAPI.Features.Spotify.Queries.GetFavourites;

public record GetFavouritesQuery : IRequest<IEnumerable<TrackDTO>>, IRequireSpotifyAuth;