using Microsoft.AspNetCore.Identity;
using PetVetApp.DTOs;

namespace PetVetApp.Models
{
    public class Pet
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
        public IdentityUser? User { get; set; }
        public ICollection<Med>? Meds { get; set; }

        public Pet ConvertFromDTO(PetDTO petDTO)
        {
            Id = petDTO.Id;
            Name = petDTO.Name;
            AnimalType = petDTO.AnimalType;
            Breed = petDTO.Breed;
            BirthDate = petDTO.BirthDate;
            Weight = petDTO.Weight;
            Height = petDTO.Height;
            Color = petDTO.Color;
            Observation = petDTO.Observation;
            ImageUrl = petDTO.ImageUrl;
            UserId = petDTO.UserId;
            Meds = petDTO.Meds?.Select(medDTO => new Med().ConvertFromDTO(medDTO)).ToList();

            return this;
        }
    }
}
