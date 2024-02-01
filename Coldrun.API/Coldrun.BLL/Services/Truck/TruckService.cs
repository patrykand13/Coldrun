using Coldrun.BLL.Interfaces.Truck;
using Coldrun.BLL.Validators.Truck;
using Coldrun.DAL.Entities;
using Coldrun.DAL.Interfaces.Truck;
using Microsoft.AspNetCore.Mvc;

namespace Coldrun.BLL.Services.Truck
{
    public class TruckService : ITruckService
    {
        private readonly ITruckRepository _truckRepository;
        private readonly TruckValidator _truckValidator;
        public TruckService(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
            _truckValidator = new TruckValidator();
        }
        public IEnumerable<TruckEntity> GetTrucks()
        {
            return _truckRepository.GetTrucks();
        }

        public TruckEntity GetTruckById(Guid id)
        {
            return _truckRepository.GetTruckById(id);
        }
        public IActionResult GetTrucksByStatus(string status)
        {
            try
            {
                _truckValidator.ValidateTruckState(status);

                var trucks = _truckRepository.GetTrucksByStatus(status);
                return new OkObjectResult(trucks);
            }
            catch (InvalidOperationException ex)
            {
                return new BadRequestObjectResult(new { ErrorMessage = ex.Message });
            }
        }
        public IActionResult CreateTruck(TruckEntity truck)
        {
            try
            {
                ValidateUniqueAlphaCode(truck.AlphaCode);
                _truckValidator.ValidateTruckFields(truck);
                _truckValidator.ValidateTruckState(truck.State);
                _truckRepository.CreateTruck(truck);

                return new OkObjectResult(truck);
            }
            catch (InvalidOperationException ex)
            {
                return new BadRequestObjectResult(new { ErrorMessage = ex.Message });
            }
        }

        public IActionResult UpdateTruck(Guid id, TruckEntity truck)
        {
            try
            {
                ValidateUniqueAlphaCode(truck.AlphaCode, id);
                _truckValidator.ValidateTruckFields(truck);
                _truckValidator.ValidateTruckState(truck.State);
                _truckRepository.UpdateTruck(id, truck);

                return new OkObjectResult(truck);
            }
            catch (InvalidOperationException ex)
            {
                return new BadRequestObjectResult(new { ErrorMessage = ex.Message });
            }
        }

        public IActionResult UpdateTruckStatus(Guid id, string newStatus)
        {
            try
            {
                _truckValidator.ValidateTruckState(newStatus);
                var truck = _truckRepository.GetTruckById(id);

                if (truck == null)
                    throw new InvalidOperationException("Truck not found");

                _truckValidator.ValidateTruckChangeState(truck.State, newStatus);


                truck.State = newStatus;
                _truckRepository.UpdateTruck(id, truck);

                return new OkObjectResult(truck);
            }
            catch (InvalidOperationException ex)
            {
                return new BadRequestObjectResult(new { ErrorMessage = ex.Message });
            }

        }

        public IActionResult DeleteTruck(Guid id)
        {
            try
            {
                var truck = _truckRepository.GetTruckById(id);

                if (truck == null)
                    throw new InvalidOperationException("Truck not found");

                _truckRepository.DeleteTruck(truck);

                return new OkResult();
            }
            catch (InvalidOperationException ex)
            {
                return new BadRequestObjectResult(new { ErrorMessage = ex.Message });
            }

        }
        private void ValidateUniqueAlphaCode(string alphaCode, Guid? excludeId = null)
        {
            var isAlphaCodeUnique = !_truckRepository.AnyTruckWithAlphaCode(alphaCode, excludeId);

            if (!isAlphaCodeUnique)
            {
                throw new InvalidOperationException("Alphanumeric code must be unique.");
            }
        }
    }
}
