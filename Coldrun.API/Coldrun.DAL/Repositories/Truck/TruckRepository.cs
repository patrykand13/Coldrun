using Coldrun.DAL.Context;
using Coldrun.DAL.Entities;
using Coldrun.DAL.Interfaces.Truck;

namespace Coldrun.DAL.Repositories.Truck
{
    public class TruckRepository : ITruckRepository
    {
        private readonly AppDbContext _context;
        public TruckRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<TruckEntity> GetTrucks()
        {
            return _context.Trucks.OrderBy(t => t.State).ToList();
        }
        public TruckEntity GetTruckById(Guid id)
        {
            return _context.Trucks.FirstOrDefault(t => t.Id == id);
        }
        public IEnumerable<TruckEntity> GetTrucksByStatus(string status)
        {
            return _context.Trucks.Where(t => t.State == status).ToList();
        }
        public bool AnyTruckWithAlphaCode(string alphaCode, Guid? excludeId = null)
        {
            return _context.Trucks.Any(t => t.AlphaCode == alphaCode && t.Id != excludeId);
        }
        public void CreateTruck(TruckEntity truck)
        {
            _context.Trucks.Add(truck);
            _context.SaveChanges();
        }

        public void UpdateTruck(Guid id, TruckEntity truck)
        {
            var existingTruck = _context.Trucks.FirstOrDefault(t => t.Id == id);

            if (existingTruck != null)
            {
                existingTruck.AlphaCode = truck.AlphaCode;
                existingTruck.Name = truck.Name;
                existingTruck.State = truck.State;
                existingTruck.Description = truck.Description;
            }
            _context.SaveChanges();
        }

        public void DeleteTruck(TruckEntity truck)
        {
            _context.Trucks.Remove(truck);
            _context.SaveChanges();
        }
    }
}
