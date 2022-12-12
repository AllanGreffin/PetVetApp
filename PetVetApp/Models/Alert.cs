using PetVetApp.DTOs;

namespace PetVetApp.Models
{
    public class Alert
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public Med? Med { get; set; }
        public Guid MedId { get; set; }
        public string? AlertType { get; set; }

        public Alert ConvertFromDTO(AlertDTO alertDTO)
        {
            Id = alertDTO.Id.Value;
            Name = alertDTO.Name;
            Date = alertDTO.Date;
            MedId = alertDTO.MedId.Value;

            return this;
        }
    }
}
