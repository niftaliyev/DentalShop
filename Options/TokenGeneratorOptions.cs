using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Options
{
    public class TokenGeneratorOptions
    {
        public string Secret { get; set; }
        public TimeSpan AccessExpiration { get; set; }
        public TimeSpan RefreshExpiration { get; set; }
    }
}
