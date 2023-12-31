﻿using System;
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
using System.Net.Sockets;

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


                    try
                    {

                        int senserID = GetID(this.Text);
                        int recipientID = GetID(recipient);
                        decimal am = amount;
                        string DT = DateTime.Now.ToString();

                        string queryTransaction = "INSERT INTO Transactions (sender_id, recipient_id, amount, transaction_datetime) VALUES (@sender_id, @recipient_id, @amount, @transaction_datetime)";
                        using (command = new SQLiteCommand(queryTransaction, connection))
                        {
                            command.Parameters.AddWithValue("@sender_id", senserID);
                            command.Parameters.AddWithValue("@recipient_id", recipientID);
                            command.Parameters.AddWithValue("@amount", am);
                            command.Parameters.AddWithValue("@transaction_datetime", DT);
                            command.ExecuteNonQuery();
                            MessageBox.Show($"Средства были отправлены: \n Id отправителя{GetID(this.Text)}; Id получателя {GetID(recipient)}; Веремя отправки{DT}; Сумма отправки: {amount}");

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не получилось сохранить инофрмацию о переводе: {ex.Message}");
                    }

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
            catch (Exception ex)
            {
                MessageBox.Show($"Ошбика получения ID: {ex.Message}");
                return -1;
            }


        }



        private void button2_Click(object sender, EventArgs e)
        {
            int senderID = GetID(this.Text);
            string senderUsername = this.Text;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string query = "Insert into Request (sender_id, sender_username) values (@sender_id, @sender_username)";
                try
                {
                    using (command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@sender_id", senderID);
                        command.Parameters.AddWithValue("@sender_username", senderUsername);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Зарос был отправлен");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не получилось отправить запрос: {ex.Message}");
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string usr = this.Text;
                string query = "select * from AccountReport where name_of_user = @username";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", usr);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        listBox1.Items.Clear();

                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                string columnValue = reader[i].ToString();
                                string message = $"{columnName}: {columnValue}";
                                MessageBox.Show(message);

                                listBox1.Items.Add(message);
                            }
                        }
                    }
                }
            }
        }
    }
}