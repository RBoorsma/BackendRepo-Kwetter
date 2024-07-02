using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
builder.Host.UseContentRoot(Directory.GetCurrentDirectory())
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config
            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
            .AddJsonFile("ocelot.json")
            .AddEnvironmentVariables();
    })   .ConfigureServices(s =>
    {
        s.AddOcelot();
        s.AddControllers();
        s.AddSwaggerForOcelot(configuration);
        s.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerForOcelotUI();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAnyOrigin");
app.UseOcelot().Wait();
app.Run();

