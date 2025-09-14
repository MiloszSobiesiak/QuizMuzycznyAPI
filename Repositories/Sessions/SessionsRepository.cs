using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using QuizMuzycznyAPI.Config;
using QuizMuzycznyAPI.Features.Sessions.Models;
using QuizMuzycznyAPI.Features.Users.Models;

namespace QuizMuzycznyAPI.Repositories;

public class SessionsRepository(AppDbContext db) : ISessionsRepository
{
    public async Task CreateSessionAsync(string sessionId, User user)
    {
        var session = new Session()
        {
            SessionId = sessionId,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        db.Sessions.Add(session);
        await db.SaveChangesAsync();
    }

    public async Task<User?> GetUserBySessionIdAsync(string sessionId)
    {
        var session = await db.Sessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.SessionId == sessionId && s.ExpiresAt > DateTime.UtcNow);

        return session?.User;
    }

    public async Task DeleteSessionAsync(string sessionId)
    {
        var session = await db.Sessions.FirstOrDefaultAsync(s => s.SessionId == sessionId);
        if (session != null)
        {
            db.Sessions.Remove(session);
            await db.SaveChangesAsync();
        }
    }
}