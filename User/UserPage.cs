using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace User
{
    public partial class UserPage : Form
    {
        private const string ConnectionString = "Data Source=Bank.db;Version=3;";
        private static SQLiteCommand command;

        public UserPage(string userName)
        {
            InitializeComponent();

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT account_balance FROM User WHERE username = @username";
                    using (command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", userName);

                        object result = command.ExecuteScalar();
                        int balance = Convert.ToInt32(result);

                        textBox3.Text = balance.ToString();
                        this.Text = userName;
                        connection.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void UserPage_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal amount = decimal.Parse(textBox1.Text);
            string recipient = textBox2.Text;

            SendMoney(recipient, amount);
        }

        private void SendMoney(string recipient, decimal amount)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT account_balance FROM User WHERE username = @username";
                    using (command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", this.Text);
                        decimal SenderBalance = (decimal)command.ExecuteScalar();

                        if (SenderBalance < amount)
                        {
                            MessageBox.Show("Недостаточно средств");
                            return;
                        }
                    }

                    string querySender = "UPDATE User SET account_balance = account_balance - @amount WHERE username = @username";
                    using (command = new SQLiteCommand(querySender, connection))
                    {
                        command.Parameters.AddWithValue("@amount", amount);
                        command.Parameters.AddWithValue("@username", this.Text);
                        command.ExecuteNonQuery();
                    }

                    string queryRecipient = "UPDATE User SET account_balance = account_balance + @amount WHERE username = @username";
                    using (command = new SQLiteCommand(queryRecipient, connection))
                    {
                        command.Parameters.AddWithValue("@amount", amount);
                        command.Parameters.AddWithValue("@username", recipient);
                        command.ExecuteNonQuery();
                    }

                    string DT = DateTime.Now.ToString();

                    try
                    {

                        string queryTransaction = "insert into [Transactions] (sender_id, recipient_id, amount, transaction_datetime) VALUES (@sender_id, @recipient_id, @amount, @transaction_datetime)";
                        using (command = new SQLiteCommand(queryTransaction, connection))
                        {
                            command.Parameters.AddWithValue("@sender_id", GetID(this.Text));
                            command.Parameters.AddWithValue("@recipient_id", GetID(recipient));
                            command.Parameters.AddWithValue("@amount", amount);
                            command.Parameters.AddWithValue("@transaction_datetime", DT);
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не получилось сохранить инофрмацию о переводе: {ex.Message}");
                    }

                    MessageBox.Show($"Id отправителя{GetID(this.Text)}; Id получателя {GetID(recipient)}; Веремя отправки{DT}; Сумма отправки: {amount}");
                }


            }
            catch (Exception ex) { MessageBox.Show($"Не получилось перевести денег ошибка: {ex.Message}"); }
        }

        private int GetID(string username)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT user_id FROM User WHERE username = @username";
                    using (command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }

                        return -1;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошбика получения ID: {ex.Message}");
                return -1;
            }

           
        }
    }
}
