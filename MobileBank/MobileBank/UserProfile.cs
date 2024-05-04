using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBank
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [PrimaryKey, AutoIncrement]
        public int profile_id { get; set; }
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set;}
        public string sec_name { get; set;}
        public DateTime birth_date { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public DateTime registration_date { get; set; }
        public string employment_type { get; set; }
       
    }
}
