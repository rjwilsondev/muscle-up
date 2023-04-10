using System.Data.SqlClient;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MuscleUp.Data;

public class Workout
{
    public int Id { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }

    public List<Exercise> Exercises { get; set; } = new List<Exercise>();
}

public class WorkoutExercise
{
    public int Id { get; set; }
    public int WorkoutId { get; set; }
    public int ExerciseId { get; set; }
}

public class Exercise
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string BodyPart { get; set; }
}

public class WorkoutContext : DbContext
{
    public WorkoutContext(DbContextOptions<WorkoutContext> options)
        : base(options)
    { }

    public DbSet<Workout> Workouts => this.Set<Workout>();
    public DbSet<Exercise> Exercises => this.Set<Exercise>();
    public DbSet<WorkoutExercise> WorkoutExercises => this.Set<WorkoutExercise>();
}

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WorkoutContext>
{
    public WorkoutContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@Directory.GetCurrentDirectory() + "/../MuscleUp.Api/local.appsettings.json")
            .Build();
        var builder = new DbContextOptionsBuilder<WorkoutContext>();
        var connection = configuration.GetConnectionString("WebApiDatabase");

        builder.UseNpgsql(connection);
        return new WorkoutContext(builder.Options);
    }
}
