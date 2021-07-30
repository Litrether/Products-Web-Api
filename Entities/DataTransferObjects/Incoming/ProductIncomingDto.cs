using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Incoming
{
    public class ProductIncomingDto
    {
        [Required(ErrorMessage = "Product name is a required field.")]
        [MaxLength(75, ErrorMessage = "Maximum length for the name is 75 characters.")]
        public string Name { get; set; }

        [MaxLength(300, ErrorMessage = "Maximum length for the description is 300 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Product name is a required field.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product cost should be upper 0.01$")]
        public double Cost { get; set; }

        [Required(ErrorMessage = "Provider id name is a required field.")]
        public int ProviderId { get; set; }

        [Required(ErrorMessage = "Category id is a required field.")]
        public int CategoryId { get; set; }
    }
}
