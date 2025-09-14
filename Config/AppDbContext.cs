using Microsoft.EntityFrameworkCore;
using QuizMuzycznyAPI.Features.Sessions.Models;
using QuizMuzycznyAPI.Features.Users.Models;

namespace QuizMuzycznyAPI.Config;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Session> Sessions => Set<Session>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SpotifyId).IsRequired();
            entity.Property(e => e.DisplayName).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.LastLogin).IsRequired();
            entity.Property(e => e.SpotifyAccessToken).IsRequired();
            entity.Property(e=> e.SpotifyRefreshToken).IsRequired();
            entity.Property(e =>e.SpotifyTokenExpiry).IsRequired();
        });
        
        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.SessionId).IsRequired();
            entity.Property(s => s.CreatedAt).IsRequired();
            entity.Property(s => s.ExpiresAt).IsRequired();
            entity.HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(s => s.SessionId).IsUnique();
        });
    }
}