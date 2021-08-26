using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Models.DTO
{
    public class AccountLoginDTO
    {
        [Required(ErrorMessage = "tetete")]
        public string Email { get; set; }
        [Required(ErrorMessage = "fasgag")]
        public string Password { get; set; }
    }
}
