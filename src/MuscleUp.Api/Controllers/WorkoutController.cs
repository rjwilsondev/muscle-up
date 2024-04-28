using System.Net;
using Microsoft.AspNetCore.Mvc;
using MuscleUp.Api.Services;
using MuscleUp.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace MuscleUp.Api.Controllers;

[Produces("application/json")]
public class WorkoutController : Controller
{
    private readonly IWorkoutService workoutService;

    public WorkoutController(IWorkoutService workoutService)
    {
        this.workoutService= workoutService;
    }



    [HttpGet(@"/workouts/{id:int}")]
    [Produces("application/json")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Test", typeof(Workout))]
    public async Task<IActionResult> GetWorkoutById(int id)
    {
        var workout = await this.workoutService.GetWorkoutById(id);
        if (workout is null)
        {
            return this.NotFound();
        }

        return this.Ok(workout);
    }

    ///<summary>This is a summary</summary>
    [HttpGet(@"/workouts")]
    [Produces("application/json")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Test", typeof(Workout))]
    public async Task<IActionResult> GetWorkouts()
    {
        var workouts = await this.workoutService.GetWorkouts();
        return this.Ok(workouts);
    }

    [HttpPost(@"/workouts")]
    [Produces("application/json")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Test", typeof(Workout))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "The data is invalid")]
    public async Task<IActionResult> CreateWorkout([FromBody] Workout workout)
    {
        var newWorkout = await this.workoutService.CreateWorkout(workout);
        return this.Created(nameof(Workout), newWorkout);
    }


    [HttpPut(@"/workouts/{id:int}")]
    [Produces("application/json")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Test", typeof(Workout))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "The data is invalid")]
    public async Task<IActionResult> UpdateWorkout(int id, [FromBody] Workout workout)
    {
        workout.Id = id;
        var newWorkout = await this.workoutService.UpdateWorkout(workout);
        return this.Ok(newWorkout);
    }


    [HttpDelete(@"/workouts/{id:int}")]
    [Produces("application/json")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Test", typeof(Workout))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "The data is invalid")]
    public async Task<IActionResult> DeleteWorkout(int id)
    {
        await this.workoutService.DeleteWorkout(id);
        return this.NoContent();
    }


}
