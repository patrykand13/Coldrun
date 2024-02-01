using Coldrun.BLL.Interfaces.Truck;
using Coldrun.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Coldrun.API.Controllers.Truck
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckController : ControllerBase
    {
        private readonly ITruckService _truckService;

        public TruckController(ITruckService truckService)
        {
            _truckService = truckService;
        }
        [HttpGet]
        public IActionResult GetTrucks()
        {
            try
            {
                var trucks = _truckService.GetTrucks();

                return Ok(trucks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTruckById(Guid id)
        {
            try
            {
                var truck = _truckService.GetTruckById(id);

                if (truck == null)
                    return NotFound();

                return Ok(truck);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("bystatus")]
        public IActionResult GetTrucksByStatus([FromQuery] string status)
        {
            try
            {
                var response = _truckService.GetTrucksByStatus(status);
                return response;
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateTruck([FromBody] TruckEntity truck)
        {
            try
            {
                var response = _truckService.CreateTruck(truck);
                return response;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTruck(Guid id, [FromBody] TruckEntity truck)
        {
            try
            {
                var response = _truckService.UpdateTruck(id, truck);
                return response;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPatch("{id}/status")]
        public IActionResult UpdateTruckStatus(Guid id, [FromBody] string newStatus)
        {
            try
            {
                var response = _truckService.UpdateTruckStatus(id, newStatus);
                return response;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTruck(Guid id)
        {
            try
            {
                var response = _truckService.DeleteTruck(id);
                return response;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
