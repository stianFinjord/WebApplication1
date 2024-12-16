using Microsoft.Extensions.Configuration;
using System.Xml;
using Dapper;
using Microsoft.Data.SqlClient;
using WebApplication1;
using Microsoft.AspNetCore.Mvc;


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


//app.MapGet("/weatherforecast", () =>
//{
//    Car car1 = new Car("Mercedes", 5);
//    return car1;
//});
const string connStr = "Server=tcp:getturi.database.windows.net,1433;Initial Catalog=TuriDB;Persist Security Info=False;User ID=getturi;Password=GetAcademy2024;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

app.MapGet("/GetCar", DataManager.GetCar);
app.MapGet("/GetUser", (int id) => DataManager.GetUser(id));

app.MapGet("/GetAllUsers", async () =>
{
    const string sql = "SELECT * FROM userProfile";
    var conn = new SqlConnection(connStr);
    var userObjects = await conn.QueryAsync(sql);
    
    return Results.Json(userObjects);
});

app.MapGet("/GetUser/{id}", async (int id) =>
{   
    const string sql = "SELECT * FROM userProfile WHERE Id = @Id";
    var conn = new SqlConnection(connStr);
    var userObject = await conn.QueryAsync(sql, new { Id = id });
    return Results.Json(userObject);
});


app.MapPost("/AcceptFriendRequest", async ([FromBody] FriendRequest friendRequest) =>
{
    const string createFriendship = @"
        INSERT INTO Friends (UserID, FriendID)  
        VALUES(@FromProfileId, @ToProfileId);
        INSERT INTO Friends (UserID, FriendID)
        VALUES(@ToProfileId, @FromProfileId);
        ";

    var conn = new SqlConnection(connStr);
    await conn.ExecuteAsync(createFriendship, new
    {
        FromProfileId = friendRequest.fromProfileId,
        ToProfileId = friendRequest.toProfileId
    });
    return Results.Json(new { Success = true });
});


//app.MapGet("/GetUser", DataManager.GetUser);
app.UseCors(); // Enable CORS (Cross Origin Resource Sharing) Allows access from different url's 
app.Run();


public class FriendRequest
{
    public int toProfileId { get; set; }
    public int fromProfileId { get; set; }
};