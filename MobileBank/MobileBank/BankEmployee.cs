using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBank
{
    [Table("BankEmployee")]
    public class BankEmployee
    {
        [PrimaryKey, AutoIncrement]
        public int employee_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set;}
        public string sec_name { get; set; }
        public string username { get; set; }
        public string password { get; set; }

    }
}
