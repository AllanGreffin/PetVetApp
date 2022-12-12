using PetVetApp.Models;

namespace PetVetApp.DTOs
{
    public class MedDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? PetId { get; set; }
        public ICollection<AlertDTO>? Alerts { get; set; }
    }
}
