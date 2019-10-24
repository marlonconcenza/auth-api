using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace auth_infra.Models
{
    public class Permission
    {
        public int id { get; set; }
        public string permission { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }
        public int accountId { get; set; }
    }
}
