﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Models.DTO
{
    public class AccountCredentialsDTO
    {
        [Required(ErrorMessage = "name not be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "tetete")]
        public string Email { get; set; }
        [Required(ErrorMessage = "fasgag")]
        public string Password { get; set; }

        [Required(ErrorMessage = "phone not be empty")]
        public string Phone { get; set; }
    }
}
