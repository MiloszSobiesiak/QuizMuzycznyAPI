using MediatR;
using QuizMuzycznyAPI.Features.Games.Models;

namespace QuizMuzycznyAPI.Features.Games.Commands.CreateGame;

public record CreateGameCommand(GameConfig Config) : IRequest<GameCreatedResponse>;