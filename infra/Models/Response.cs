using System;

namespace auth_infra.Models
{
    public class Response
    {
        public int id { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public string path { get; set; }
    }
}
