using Microsoft.AspNetCore.Mvc;
using MuscleUp.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace MuscleUp.Api.Controllers;

[Route("workout")]
public class WorkoutController : Controller
{
    private readonly WorkoutContext context;

    public WorkoutController(WorkoutContext context) => this.context = context;


    ///<summary>This is a summary</summary>
    [HttpGet("")]
    [Produces("application/json")]
    [SwaggerResponse(201, "Test", typeof(string))]
    [SwaggerResponse(400, "The data is invalid")]
    public string GetWorkouts()
    {

        var workouts = this.context.Workouts.ToList();

        return workouts.Count.ToString();
    }
}
