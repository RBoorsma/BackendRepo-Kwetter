using Kwetter.Library.Messaging.Enums;
using Messaging;
using UserProfileService.Core;
using UserProfileService.Core.Messaging.Handler;
using UserProfileService.Core.Messaging.RabbitMQ;
using UserProfileService.Core.Profiles;
using UserProfileService.Core.Service;
using UserProfileService.DAL.Context;
using UserProfileService.DAL.Repository;
using UserProfileService.DAL.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddTransient<IUserProfileService, UserProfileServiceCore>();
builder.Services.AddTransient<IRabbitMqPublisher, RabbitMqPublisher>();
builder.Services.AddTransient<IRabbitMqConsumer, RabbitMqConsumer>();
builder.Services.AddTransient<IMessageHandler, MessageHandler>();
builder.Services.AddDbContext<UserProfileDbContext>();
builder.Services.AddAutoMapper(typeof(UserProfilesProfile));

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
IMessageHandler messageBus = app.Services.GetRequiredService<IMessageHandler>();
Task.Run(() =>
{
    messageBus.StartListening();
});


app.Run();

