using System;

namespace auth_infra.Models
{
    public class Auth
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string token { get; set; }
    }
}
