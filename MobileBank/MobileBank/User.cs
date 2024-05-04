using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MobileBank
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public decimal account_balance { get; set; }

        public static int GetNewId()
        {
            return App.Db.GetUseкIDCount() + 1;
        }
    }
}

