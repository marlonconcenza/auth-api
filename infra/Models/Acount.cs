using System;

namespace auth_infra.Models
{
    public class Acount
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime createdAt { get; set; }
        public string role { get; set; }
    }
}
