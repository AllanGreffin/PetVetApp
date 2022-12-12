using PetVetApp.Models;

namespace PetVetApp.DTOs
{
    public class AlertDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public Guid? MedId { get; set; }
    }
}
