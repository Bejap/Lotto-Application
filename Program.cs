using LottoApplication.Services;

namespace LottoApplication;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddSingleton<ILottoService, LottoService>();

        // Add Swagger/OpenAPI support
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
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
