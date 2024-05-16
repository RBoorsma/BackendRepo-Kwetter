using System.Configuration;
using Microsoft.EntityFrameworkCore;
using UserService.Core.Messaging;
using UserService.Core.Profiles;
using UserService.Core.Services;
using UserService.Core.Messaging.RabbitMQ;
using UserService.Core.Messaging.Handler;
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
builder.Services.AddTransient<IRabbitMqPublisher, RabbitMqPublisher>();
builder.Services.AddTransient<IRabbitMqConsumer, RabbitMqConsumer>();
builder.Services.AddTransient<IMessageHandler, MessageHandler>();
builder.Services.AddDbContext<UserDbContext>();
builder.Services.AddAutoMapper(typeof(UserProfile));

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

