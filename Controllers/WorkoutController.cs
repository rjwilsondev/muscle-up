using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

[Route("workout")]
public class WorkoutController : Controller
{

    ///<summary>This is a summary</summary>
    [HttpGet("")]
    [SwaggerResponse(201, "Test", typeof(string))]
    [SwaggerResponse(400, "The data is invalid")]
    public string GetWorkouts()
    {
        return "Hello Ryan!";
    }
}