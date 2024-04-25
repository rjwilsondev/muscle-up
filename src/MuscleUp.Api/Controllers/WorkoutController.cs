using System.Net;
using Microsoft.AspNetCore.Mvc;
using MuscleUp.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace MuscleUp.Api.Controllers;

[Route("workouts")]
public class WorkoutController : Controller
{
    private readonly WorkoutContext context;

    public WorkoutController(WorkoutContext context) => this.context = context;


    ///<summary>This is a summary</summary>
    [HttpGet()]
    [Produces("application/json")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Test", typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "The data is invalid")]
    public async Task<IActionResult> GetWorkouts()
    {
        var workouts = this.context.Workouts.ToList();
        return this.Ok(workouts);
    }

    [HttpPost()]
    [Produces("application/json")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Test", typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "The data is invalid")]
    public async Task<IActionResult> CreateWorkout(Workout workout)
    {
        workout.Id = 0;
        this.context.Workouts.Add(workout);
        await this.context.SaveChangesAsync();
        return this.Created(nameof(Workout), workout);
    }
}
