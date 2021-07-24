using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Incoming
{
    public class UserValidationDto
    {
        [Required(ErrorMessage = "Username is required field.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required field.")]
        public string Password { get; set; }
    }
}
