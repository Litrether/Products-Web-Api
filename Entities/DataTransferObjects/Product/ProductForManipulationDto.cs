using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class ProductForManipulationDto
    {
        [Required(ErrorMessage = "Product name is a required field.")]
        [MaxLength(75, ErrorMessage = "Maximum length for the name is 75 characters.")]
        public string Name { get; set; }

        [MaxLength(300, ErrorMessage = "Maximum length for the description is 300 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Product name is a required field.")]
        [Range(0.01, 10000000, ErrorMessage = "Product cost can't be lower than 0.01$ and above than 10000000$.")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Provider id name is a required field.")]
        public int ProviderId { get; set; }

        [Required(ErrorMessage = "Category id is a required field.")]
        public int CategoryId { get; set; }

    }
}
