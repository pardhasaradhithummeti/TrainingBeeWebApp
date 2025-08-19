using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingCentreWebApi.Models;

namespace TrainingCentreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        TrainingCentreDbContext Db = null;
        [HttpGet]
        public IActionResult GetLocations()
        {
            if (Db == null)
            {
                return NotFound("Database context is not initialized.");
            }
            var locations = Db.TrainingCentres.ToList();
            return Ok(locations);
        }
        [HttpGet("{id}")]
        public IActionResult GetLocationById(int id)
        {
            if (Db == null)
            {
                return NotFound("Database context is not initialized.");
            }
            var location = Db.TrainingCentres.FirstOrDefault(l => l.LocationId == id);
            if (location == null)
            {
                return NotFound($"Location with ID {id} not found.");
            }
            return Ok(location);
        }
        [HttpPost]
        public IActionResult AddLocation([FromBody] Location location)
        {
            if (Db == null)
            {
                return NotFound("Database context is not initialized.");
            }
            if (location == null)
            {
                return BadRequest("Location data is null.");
            }
            Db.TrainingCentres.Add(location);
            Db.SaveChanges();
            return CreatedAtAction(nameof(GetLocationById), new { id = location.LocationId }, location);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLocation(int id, [FromBody] Location location)
        {
            if (Db == null)
            {
                return NotFound("Database context is not initialized.");
            }
            if (location == null || location.LocationId != id)
            {
                return BadRequest("Location data is invalid.");
            }
            var existingLocation = Db.TrainingCentres.FirstOrDefault(l => l.LocationId == id);
            if (existingLocation == null)
            {
                return NotFound($"Location with ID {id} not found.");
            }
            existingLocation.CentreName = location.CentreName;
            existingLocation.ContactNumber = location.ContactNumber;

            Db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLocation(int id)
        {
            if (Db == null)
            {
                return NotFound("Database context is not initialized.");
            }
            var location = Db.TrainingCentres.FirstOrDefault(l => l.LocationId == id);
            if (location == null)
            {
                return NotFound($"Location with ID {id} not found.");
            }
            Db.TrainingCentres.Remove(location);
            Db.SaveChanges();
            return NoContent();
        }


    }
}
