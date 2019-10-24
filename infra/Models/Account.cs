using System;
using System.Collections.Generic;

namespace auth_infra.Models
{
    public class Account
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime createdAt { get; set; }
        public string role { get; set; }
        public IEnumerable<Permission> permissions { get; set; }
    }
}
