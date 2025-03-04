using Microsoft.AspNetCore.Mvc;
using CalculateTimeAngle.Services;

namespace CalculateTimeAngle.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculateTimeAngleController : ControllerBase
{
    private readonly ILogger<CalculateTimeAngleController> _logger;
    private readonly TimeAngleService _timeAngleService;

    public CalculateTimeAngleController(ILogger<CalculateTimeAngleController> logger, TimeAngleService timeAngleService)
    {
        _logger = logger;
        _timeAngleService = timeAngleService;
    }

    [HttpGet("{hour}/{minute}")]
    public ActionResult<string> Get(int hour, int minute)
    {
        return ProcessValues(hour, minute);
    }

    [HttpGet("{time}")]
    public ActionResult<string> Get(string time)
    {
        string[] times = time.Split(':');
        if (times.Length == 2 && int.TryParse(times[0], out int hour) && int.TryParse(times[1], out int minute))   
        {
            return ProcessValues(hour, minute);
        } else {
            return BadRequest("Invalid time format use H:m, HH:mm or combination");
        }
    }

    private ActionResult<string> ProcessValues(int hour, int minute)
    {
        // hours can be reduced to < 12 and min must be <= 60

        int degree = _timeAngleService.Calculate(hour, minute);
        return Ok($"{degree} degrees");
    }
}
