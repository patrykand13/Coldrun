using Coldrun.DAL.Entities;
using System.Text.RegularExpressions;

namespace Coldrun.BLL.Validators.Truck
{
    internal class TruckValidator
    {
        private readonly Dictionary<string, List<string>> _validStatusTransitions;

        internal TruckValidator()
        {
            _validStatusTransitions = new Dictionary<string, List<string>>
            {
                { "Out Of Service", new List<string> { "Out Of Service", "Loading", "To Job", "At Job", "Returning" } },
                { "Loading", new List<string> { "Out Of Service", "Returning" } },
                { "To Job", new List<string> { "Out Of Service", "Loading" } },
                { "At Job", new List<string> { "Out Of Service", "To Job" } },
                { "Returning", new List<string> { "Out Of Service", "At Job" } }
            };
        }

        internal void ValidateTruckFields(TruckEntity truckEntity)
        {
            if (string.IsNullOrWhiteSpace(truckEntity.Name))
                throw new InvalidOperationException("Truck Name is required");

            if (string.IsNullOrWhiteSpace(truckEntity.AlphaCode))
                throw new InvalidOperationException("Truck AlphaCode is required");
            ValidateAlphaCode(truckEntity.AlphaCode);
        }
        internal void ValidateTruckState(string state)
        {
            if (!_validStatusTransitions.ContainsKey(state))
                throw new InvalidOperationException($"Invalid truck state: {state}");
        }
        internal void ValidateTruckChangeState(string currentStatus, string newStatus)
        {
            if (!_validStatusTransitions[newStatus].Contains(currentStatus))
                throw new InvalidOperationException($"Invalid status transition from '{currentStatus}' to '{newStatus}'");
        }
        private void ValidateAlphaCode(string alphaCode)
        {
            if (!Regex.IsMatch(alphaCode, "^[a-zA-Z0-9]+$"))
                throw new InvalidOperationException("AlphaCode must be alphanumeric.");
        }
    }
}
