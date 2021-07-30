using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.Incoming
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "User first name is a required field.")]
        [MaxLength(75, ErrorMessage = "Maximum length for the first name is 75 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "User last name is a required field.")]
        [MaxLength(75, ErrorMessage = "Maximum length for the last name is 75 charactesrs.")]
        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
