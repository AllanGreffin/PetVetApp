using PetVetApp.DTOs;

namespace PetVetApp.Models
{
    public class Med
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Pet? Pet { get; set; }
        public Guid PetId { get; set; }
        public ICollection<Alert>? Alerts { get; set; }

        public Med ConvertFromDTO(MedDTO medDTO)
        {
            Id = medDTO.Id.Value;
            Name = medDTO.Name;
            Description = medDTO.Description;
            StartDate = medDTO.StartDate;
            EndDate = medDTO.EndDate;
            PetId = medDTO.PetId.Value;
            Alerts = medDTO.Alerts?.Select(alertDTO => new Alert().ConvertFromDTO(alertDTO)).ToList();

            return this;
        }
    }
}
