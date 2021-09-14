using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Incoming
{
    public class CategoryIncomingDto
    {
        [Required(ErrorMessage = "Category name is a required field.")]
        [MaxLength(75, ErrorMessage = "Maximum length for the name is 75 characters.")]
        public string Name { get; set; }
    }
}