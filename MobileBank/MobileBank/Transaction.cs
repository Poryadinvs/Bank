using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBank
{
    [Table("Transaction")]
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int transaction_id { get; set; }
        public int sender_id { get; set; }
        public int recipient_id { get; set; }
        public decimal amount { get; set; }
        public DateTime transaction_datetime { get; set; }

        [Ignore]
        public User Sender { get; set; }

        [Ignore]
        public User Recipient { get; set; }
    }
}
