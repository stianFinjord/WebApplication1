using Microsoft.Extensions.Configuration;
using System.Xml;
using WebApplication1;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors(); // Enable CORS middleware

//app.MapGet("/weatherforecast", () =>
//{
//    Car car1 = new Car("Mercedes", 5);
//    return car1;
//});

app.MapGet("/GetCar", DataManager.GetCar);
app.MapGet("/GetAllUsers", DataManager.GetAllUsers);
app.MapGet("/GetUser", (int id) => DataManager.GetUser(id));

//app.MapGet("/GetUser", DataManager.GetUser);

app.Run();
