

using Microsoft.EntityFrameworkCore;
using QuizMuzycznyAPI.Config;
using QuizMuzycznyAPI.Features.Users.Commands.CreateOrUpdateUser;
using QuizMuzycznyAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "5055";
builder.WebHost.UseUrls($"http://*:{port}");

// 1. Dodaj DbContext
var connectionString = "Host=metro.proxy.rlwy.net:27283;Port=5432;Database=railway;Username=postgres;Password=YCTWDGvHJxpnaGnWjzJIGvTMldinAjsv";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2. Dodaj MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<CreateOrUpdateUserCommandHandler>());

// 3. Dodaj repozytoria
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ISessionsRepository, SessionsRepository>();

// 4. HttpClientFactory
builder.Services.AddHttpClient();

// 5. CORS tylko dla Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(
                "https://quiz-muzyczny.vercel.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// 6. Kontrolery
builder.Services.AddControllers();

var app = builder.Build();

// 7. Użyj CORS
app.UseCors("FrontendPolicy");

// 8. Middleware
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();