using Coldrun.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Coldrun.BLL.Interfaces.Truck
{
    public interface ITruckService
    {
        IEnumerable<TruckEntity> GetTrucks();
        TruckEntity GetTruckById(Guid id);
        IActionResult GetTrucksByStatus(string status);
        IActionResult CreateTruck(TruckEntity truckEntity);
        IActionResult UpdateTruck(Guid id, TruckEntity truckEntity);
        IActionResult UpdateTruckStatus(Guid id, string newStatus);
        IActionResult DeleteTruck(Guid id);
    }
}
