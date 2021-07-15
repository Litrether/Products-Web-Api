using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage = "User first name is a required field.")]
        [MaxLength(75, ErrorMessage = "Maximum length for the first name is 75 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "User last name is a required field.")]
        [MaxLength(75, ErrorMessage = "Maximum length for the last name is 75 charactesrs.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User age is a required field.")]
        [Range(18, 120, ErrorMessage = "User age can't be lower than 18 and above than 120.")]
        public int Age { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
