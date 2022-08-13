using Microsoft.OpenApi.Models;
using Serilog;
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Coin Jar API",
            Description = "Simple application for counting the collected coins",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Contact us",
                Url = new Uri("https://example.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Check License here",
                Url = new Uri("https://example.com/license")
            }
        });
    });
    // Register CORS features
    builder.Services.AddCors(options => options.AddPolicy("KnownCORS", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())); 

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger(options =>
        {
            options.SerializeAsV2 = true;
        });
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseCors("KnownCORS");

    app.MapControllers();
    Log.Information("Host starting...");
    await app.RunAsync();
    Log.Information("Host shutting down!");
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
    Log.Fatal(exception, "Error starting application! '{ErrorMessage}'", exception.Message);
}
finally
{
    Log.CloseAndFlush();
}
