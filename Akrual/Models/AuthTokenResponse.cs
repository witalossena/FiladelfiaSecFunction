using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiladelfiaFunction.Akrun.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class AuthTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expiresOn")]
        public long ExpiresOn { get; set; } 

        [JsonProperty("userInfo")]
        public UserInfo UserInfo { get; set; } 

        [JsonProperty("permissions")]
        public List<string> Permissions { get; set; } 
    }

    public class UserInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; } 
    }

}
