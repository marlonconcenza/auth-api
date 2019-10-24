using System;

namespace auth_infra.Models
{
    public class Permission
    {
        public int id { get; set; }
        public int accountId { get; set; }
        public string permission { get; set; }
    }
}
