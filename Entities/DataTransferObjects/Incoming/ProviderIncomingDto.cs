using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Incoming
{
    public class ProviderIncomingDto
    {
        [Required(ErrorMessage = "Provider name is a required field.")]
        [MaxLength(75, ErrorMessage = "Maximum length for the name is 75 characters.")]
        public string Name { get; set; }

        public decimal LocationLong { get; set; }

        public decimal LocationLat { get; set; }
    }
}
