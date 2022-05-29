using First.Database;
using First.Interface;
using First.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Db Connection String

string connString = builder.Configuration.GetConnectionString("DBConnection");

builder.Services.AddDbContext<OnlineTeachingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));

});
// Custom Store procedure Class
//builder.Services.AddDbContext<StoredProcedureClass>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));

//});

#endregion


builder.Services.AddSingleton<HelloService>(new HelloService());
builder.Services.AddScoped<ILectureService, LectureService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/", () =>
 "Hello Princi"
);

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");


app.MapGet("/test1", (HelloService helloService) => helloService.SayHello("mangla"));
/// <summary>
/// Get list of lectures
/// </summary>
app.MapGet("/lectures", (ILectureService lecture) =>
 lecture.GetLectures());

/// <summary>
/// Create lecture
/// </summary>
app.MapPost("/AddLecture", (ILectureService lectureService, Lecture lecture) =>
{
    lectureService.AddLecture(lecture);
    return Results.Created("Lecture created successfully", lecture);
}
);
app.MapControllers();

/// <summary>
/// Update lecture
/// </summary>
app.MapPut("/UpdateLecture", (ILectureService lectureService, Lecture lecture) =>
    {
        lectureService.UpdateLecture(lecture);
        return Results.NoContent();
    });

/// <summary>
/// Delete lecture
/// </summary>
app.MapDelete("/DeleteLecture", (ILectureService lectureService, Guid LectureId) =>
 {
     lectureService.RemoveLecture(LectureId);
     return Results.NoContent();
 });
app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}