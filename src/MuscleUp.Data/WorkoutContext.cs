using Microsoft.EntityFrameworkCore;
namespace Workout.Data;

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
