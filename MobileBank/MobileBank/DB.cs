using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SQLite;
using Xamarin.Forms;
namespace MobileBank
{
    public class DB
    {
       private readonly SQLiteConnection conn;

        public DB(string path)
        {
            conn = new SQLiteConnection(path);
            conn.CreateTable<User>();
            conn.CreateTable<BankEmployee>();
            conn.CreateTable<AccountReport>();
            conn.CreateTable<Transaction>();
            conn.CreateTable<UserProfile>();
            conn.CreateTable<Request>();
        }



        public List<User> GetUser()
        {
            return conn.Table<User>().ToList();
        }

        public List<BankEmployee> GetBankEmployee()
        {
            return conn.Table<BankEmployee>().ToList();
        }

        public List<AccountReport> GetAccountReport()
        {
            return conn.Table<AccountReport>().ToList();
        }

        public List<AccountReport>GetExecAccuntReport(string usernaem)
        {
           return conn.Table<AccountReport>().Where(r => r.name_of_user == usernaem).ToList();
        }

        public List<Transaction> GetTransaction()
        {
            return conn.Table<Transaction>().ToList();
        }

        public List<UserProfile> GetUserProfile()
        {
            return conn.Table<UserProfile>().ToList();
        }

        public List <Request> GetRequest()
        {
            return conn.Table<Request>().ToList();
        }

        public int SaveUser(User user)
        {
            return conn.Insert(user);
        }

        public int SaveBankEmployee(BankEmployee employee)
        {
            return conn.Insert(employee);
        }

        public int SaveAccountReport(AccountReport accountReport)
        {
            return conn.Insert(accountReport);
        }

        public int SaveTransaction(Transaction transaction)
        {
            return conn.Insert(transaction);
        }

        public int SaveUserProfile(UserProfile profile)
        {
            return conn.Insert(profile);
        }

        public int SaveRequest(Request request)
        {
            return conn.Insert(request);
        }

        public int GetUseкIDCount()
        {
            return conn.ExecuteScalar<int>("SELECT COUNT(*) FROM UserProfile");
        }

        public void DeleteAllFromUserProfile()
        {
            conn.Execute("DELETE FROM UserProfile");
        }

        public int GetUserBalance(string usr)
        {
            return conn.ExecuteScalar<int>($"SELECT account_balance FROM User WHERE username = '{usr}'");
        }

        public string GetUserName(string usr) 
        {
            return conn.ExecuteScalar<string>($"SELECT username FROM User WHERE username = '{usr}'");
        }

        public string GetBankEmployeeUsername(string usr)
        {
            return conn.ExecuteScalar<string>($"SELECT username FROM BankEmployee WHERE username = '{usr}'");
        }

        public int GetID(string usr)
        {
            return conn.ExecuteScalar<int>($"SELECT user_id FROM User WHERE username = '{usr}'");
        }

        public int GetBankEmployeeID(string usr)
        {
            return conn.ExecuteScalar<int>($"SELECT employee_id FROM BankEmployee WHERE username = '{usr}'");
        }

        public void UpdateBalance(string username, decimal newBalance)
        {
            conn.Execute("UPDATE User SET account_balance = ? WHERE username = ?", newBalance, username);
        }

        public void SendMoney(string senderUsername, string recipientUsername, decimal amount)
        {
            User sender = conn.Table<User>().FirstOrDefault(u => u.username == senderUsername);
            User recipient = conn.Table<User>().FirstOrDefault(u => u.username == recipientUsername);

            if (sender == null || recipient == null)
            {
                throw new Exception("Невозможно найти отправителя или получателя.");
            }

            if (sender.account_balance < amount)
            {
                throw new Exception("Недостаточно средств для отправки.");
            }

            sender.account_balance -= amount;
            recipient.account_balance += amount;

            UpdateBalance(senderUsername, sender.account_balance);
            UpdateBalance(recipientUsername, recipient.account_balance);

            Transaction transaction = new Transaction
            {
                sender_id = sender.user_id,
                recipient_id = recipient.user_id,
                amount = amount,
                transaction_datetime = DateTime.Now
            };

            SaveTransaction(transaction);
        }

        //public string GenerateInsertQuery(UserProfile profile)
        //{
        //    return $"INSERT INTO UserProfile (user_id, first_name, sec_name, last_name, birth_date, address, email, registration_date, employment_type) VALUES ({profile.user_id}, '{profile.first_name}', '{profile.sec_name}', '{profile.last_name}', '{profile.birth_date.ToString("yyyy-MM-dd")}', '{profile.address}', '{profile.email}', '{profile.registration_date.ToString("yyyy-MM-dd HH:mm:ss")}', '{profile.employment_type}')";
        //}

        public void DropTableAccountReport()
        {
            conn.Execute("DROP TABLE AccountReport");
        }
    }
}
