using Kwetter.Library.Messaging.Datatypes;
using Messaging.RabbitMQ;
using UserProfileService.Core.Messaging.Handler;
using UserProfileService.Core.Messaging.Models;
using UserProfileService.Core.Profiles;
using UserProfileService.Core.Service;
using UserProfileService.Core.ViewModel.ResponseBody;
using UserProfileService.DAL.Context;
using UserProfileService.DAL.Repository;
using UserProfileService.DAL.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Console.WriteLine("Newest version is running");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddTransient<IUserProfileService, UserProfileServiceCore>();
builder.Services.AddTransient(typeof(IRabbitMQReceiver<>), typeof(RabbitMQReceiver<>));
builder.Services.AddTransient<IRabbitMQPublisher, RabbitMQPublisher>();
builder.Services.AddTransient<IMessageHandler, ProfileMessageHandler>();
builder.Services.AddSingleton<IMQConnection, MQConnection>();
builder.Services.AddDbContext<UserProfileDbContext>();
builder.Services.AddAutoMapper(typeof(UserProfilesProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserProfile Service");
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


IMessageHandler handler =  app.Services.GetRequiredService<IMessageHandler>();
handler.StartListening();
handler.DeclareQueue();




app.Run();