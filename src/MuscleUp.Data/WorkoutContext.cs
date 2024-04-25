using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MuscleUp.Data;

public class Workout
{
    [Key]
    public int Id { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }

    public ICollection<Exercise> Exercises { get; set; } = [];
}

public class WorkoutExercise
{
    [Key]
    public int Id { get; set; }
    public int WorkoutId { get; set; }
    public int ExerciseId { get; set; }
}

public class Exercise
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string BodyPart { get; set; }
}

public class WorkoutContext(DbContextOptions<WorkoutContext> options) : DbContext(options)
{
    public DbSet<Workout> Workouts => this.Set<Workout>();
    public DbSet<Exercise> Exercises => this.Set<Exercise>();
    public DbSet<WorkoutExercise> WorkoutExercises => this.Set<WorkoutExercise>();
}

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WorkoutContext>
{
    public WorkoutContext CreateDbContext(string[] args)
    {
        //TODO: There has to be a better way to get the conn string right?
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@Directory.GetCurrentDirectory() + "/../MuscleUp.Api/appsettings.json")
            .Build();
        var builder = new DbContextOptionsBuilder<WorkoutContext>();
        var connection = configuration.GetConnectionString("WorkoutDatabase");

        builder.UseSqlServer(connection);
        return new WorkoutContext(builder.Options);
    }
}
