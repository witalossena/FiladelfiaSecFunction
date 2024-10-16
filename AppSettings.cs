using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiladelfiaFunction
{
    public class AkrualSettings
    {
        public string? BaseUrl { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? GrantType { get; set; }
    }

    public class FiladelfiaSettings
    {
        public string? BaseUrl { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public required string  DatabaseUrl { get; set;}
    }

}
