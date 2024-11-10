namespace VSOSH.Service;

/// <summary>
/// Класс, запускающий программу.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddAuthorization();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApiDocument();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi();
        }

        app.UseAuthorization();

        app.Run();
    }
}