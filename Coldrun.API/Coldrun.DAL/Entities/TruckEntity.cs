using System.ComponentModel.DataAnnotations;

namespace Coldrun.DAL.Entities
{
    public class TruckEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "AlphaCode is required")]
        public string AlphaCode { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        public string Description { get; set; }
    }
}
