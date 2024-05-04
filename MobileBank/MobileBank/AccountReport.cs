using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBank
{
    [Table("AccountReport")]
    public class AccountReport
    {
        [PrimaryKey, AutoIncrement]
        public int report_id { get; set; }
        public int employee_id { get; set; }
        public string name_of_user { get; set; }
        public int user_id { get; set; }
        public string TransactionInfo { get; set; }

        [Ignore]
        public BankEmployee Employee { get; set; }

        [Ignore]
        public User User { get; set; }
    }
}
