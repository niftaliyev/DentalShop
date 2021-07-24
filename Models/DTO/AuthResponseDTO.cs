using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Models.DTO
{
    public class AuthResponseDTO
    {
        public string AccessToken { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}
