using MediatR;
using QuizMuzycznyAPI.Features.Spotify.Models;
using QuizMuzycznyAPI.Features.Users.Models;

namespace QuizMuzycznyAPI.Features.Spotify.Queries.GetFavourites;

public record GetFavouritesQuery(User User) : IRequest<IEnumerable<TrackDTO>>;