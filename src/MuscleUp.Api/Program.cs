using System.Data.SqlClient;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MuscleUp.Data;

var builder = WebApplication.CreateBuilder(args);

//var conStrBuilder = new SqlConnectionStringBuilder(
//       )
//{
//    Password = builder.Configuration["DbPassword"]
//};

var connection = builder.Configuration.GetConnectionString("WebApiDatabase");

builder.Services.AddDbContext<WorkoutContext>(
    options => options.UseNpgsql(connection)
);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MuscleUp API",
        Description = "An ASP.NET Core Web API for managing Workouts items",
        // TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            // Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            // Url = new Uri("https://example.com/license")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        //options.RoutePrefix = "/swagger";
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
