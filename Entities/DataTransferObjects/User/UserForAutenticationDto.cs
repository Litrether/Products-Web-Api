using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class UserForAutenticationDto
    {
        [Required(ErrorMessage = "Username is required field.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required field.")]
        public string Password { get; set; }
    }
}
