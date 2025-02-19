using System.Configuration;
using Messaging.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using UserService.Core.Messaging;
using UserService.Core.Profiles;
using UserService.Core.Services;

using UserService.Core.Messaging.Handler;
using UserService.Core.Messaging.Models;
using UserService.DAL.Context;
using UserService.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserService, UserServiceCore>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserMessageHandler, UserMessageHandler>();
builder.Services.AddTransient(typeof(IRabbitMQReceiver<>), typeof(RabbitMQReceiver<>));
builder.Services.AddTransient<IRabbitMQPublisher, RabbitMQPublisher>();
builder.Services.AddSingleton<IMQConnection, MQConnection>();
builder.Services.AddDbContext<UserDbContext>();
builder.Services.AddAutoMapper(typeof(UserProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Service");
    });
}
// Cors
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
IUserMessageHandler handler =  app.Services.GetRequiredService<IUserMessageHandler>();
handler.StartListening();



app.Run();

