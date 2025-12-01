using LottoApplication.Services;

namespace LottoApplication;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddSingleton<LottoService>();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseAuthorization();

        app.MapControllers();

        Console.WriteLine("========================================");
        Console.WriteLine("🎰 Lotto Application Server Running");
        Console.WriteLine("========================================");
        Console.WriteLine("Press Ctrl+C to stop the server");
        Console.WriteLine("========================================");

        app.Run();
    }
}
