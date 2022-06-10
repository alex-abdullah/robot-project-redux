using Microsoft.AspNetCore.Mvc;
using WebApp2.Controller.Services;

namespace WebApp2.Controllers;

[ApiController]
[Route("[controller]")]
public class RobotSpottedController : ControllerBase // Map request to a response 
{
    private readonly ILogger<RobotSpottedController> _logger;
    private readonly LocationService _service;
    private readonly LocationContext _context;

    public RobotSpottedController(LocationContext context, LocationService service, ILogger<RobotSpottedController> logger)
    {
        _service = service;
        _logger = logger;
        _context = context;
    }    

    [HttpGet(Name = "RobotSpotted")]
    public IEnumerable<RobotSightings> Get()
    {
        return _context.Locations;
    }

    [HttpPost(Name = "RobotSpotted")]
    public async Task<string> Post(string location)
    {
        _logger.Log(LogLevel.Information, new EventId(), null, "Location name sent: " + location, null);
        Location[] final = await _service.GetNearestWaterLocation(location);
        RobotSightings newSighting = new RobotSightings();
        newSighting.sighting = location;
        //_context.Locations.Add(newSighting);
        //_context.SaveChanges();
        _logger.Log(LogLevel.Information, new EventId(), null, "Water location returned: " + final[0].display_name, null);

        return final[0].display_name;
        
    }
}

