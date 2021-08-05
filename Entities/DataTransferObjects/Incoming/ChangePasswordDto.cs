using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Incoming
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Old password is required field.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required field.")]
        public string NewPassword { get; set; }
    }
}
