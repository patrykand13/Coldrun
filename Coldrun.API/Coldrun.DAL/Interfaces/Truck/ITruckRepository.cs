using Coldrun.DAL.Entities;

namespace Coldrun.DAL.Interfaces.Truck
{
    public interface ITruckRepository
    {
        IEnumerable<TruckEntity> GetTrucks();
        TruckEntity GetTruckById(Guid id);
        IEnumerable<TruckEntity> GetTrucksByStatus(string status);
        bool AnyTruckWithAlphaCode(string alphaCode, Guid? excludeId = null);
        void CreateTruck(TruckEntity truck);
        void UpdateTruck(Guid id, TruckEntity truck);
        void DeleteTruck(TruckEntity truck);
    }
}
