using Microsoft.EntityFrameworkCore;
using MuscleUp.Data;

namespace MuscleUp.Api.Services;

public interface IWorkoutService
{
    Task<Workout> CreateWorkout(Workout workout);
    Task DeleteWorkout(int id);
    Task<List<Workout>> GetWorkouts();
    Task<Workout?> GetWorkoutById(int id);
    Task<Workout?> UpdateWorkout(Workout workout);
}

public class WorkoutService : IWorkoutService
{
    private readonly WorkoutContext context;

    public WorkoutService(WorkoutContext context)
    {
        this.context = context;
    }


    ///<summary>This is a summary</summary>

    public async Task<Workout> CreateWorkout(Workout workout)
    {
        workout.Id = 0;
        this.context.Workouts.Add(workout);
        await this.context.SaveChangesAsync();
        return workout;
    }
    public async Task DeleteWorkout(int id)
    {
        var dbWorkout = await this.GetWorkoutById(id) ?? throw new ArgumentException("No workout id that matches: " + id);
        this.context.Remove(dbWorkout);
        await this.context.SaveChangesAsync();
    }

    public async Task<List<Workout>> GetWorkouts()
    {
        return await this.context.Workouts.ToListAsync();
    }

    public async Task<Workout?> GetWorkoutById(int id)
    {
        return await this.context.Workouts.FirstOrDefaultAsync(workout => workout.Id == id);
    }

    public async Task<Workout?> UpdateWorkout(Workout workout)
    {
        var dbWorkout = await this.GetWorkoutById(workout.Id) ?? throw new ArgumentException("No workout id that matches: " + workout.Id);
        dbWorkout.StartDate = workout.StartDate;
        dbWorkout.EndDate = workout.EndDate;
        await this.context.SaveChangesAsync();
        return dbWorkout;
    }

}
