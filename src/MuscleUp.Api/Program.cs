using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MuscleUp.Api.Services;
using MuscleUp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyMuscleUpCORSPolicy",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

//var conStrBuilder = new SqlConnectionStringBuilder(
//       )
//{
//    Password = builder.Configuration["DbPassword"]
//};



/**
 * EF Core with SQL Server.
 */
var connection = builder.Configuration.GetConnectionString("WorkoutDatabase");

builder.Services.AddDbContext<WorkoutContext>(
    options => options.UseSqlServer(connection)
);



/**
 * Swagger, OpenAPI config.
 */
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




// Add services to the container.
builder.Services.AddControllers();



/**
 * Register Services for DI. 
 */
builder.Services.AddScoped<IWorkoutService, WorkoutService>();

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
app.UseCors("MyMuscleUpCORSPolicy");

app.UseAuthorization();
app.MapControllers();
app.Run();
