using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBank
{

    [Table("Request")]
    public class Request
    {
        [PrimaryKey, AutoIncrement]
        public int request_id { get; set; }
        public int sender_id { get; set; }
        public string sender_name { get; set;}
    }
}
