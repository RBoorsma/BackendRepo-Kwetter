using RegisterOrchService.Core.Messaging;
using RegisterOrchService.Core.Profiles;
using RegisterOrchService.Core.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IRegisterOrchService, RegisterOrchServiceCore>();
builder.Services.AddTransient<IRabbitMqService, RabbitMqService>();
builder.Services.AddAutoMapper(typeof(OrchestrationProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();