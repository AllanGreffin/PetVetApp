using Microsoft.AspNetCore.Identity;

namespace PetVetApp.DTOs
{
    public class PetDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string? AnimalType { get; set; }
        public string? Breed { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public string? Color { get; set; }
        public string? Observation { get; set; }
        public string? ImageUrl { get; set; }

        public Guid UserId { get; set; }
        public ICollection<MedDTO>? Meds { get; set; }
    }
}
