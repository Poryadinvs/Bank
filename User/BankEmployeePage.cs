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
using System.Data.Entity.Core.Metadata.Edm;
using System.Net.Sockets;

namespace User
{
    public partial class BankEmployeePage : Form
    {
        private TcpClient client;
        private Thread clientThread;

        private const string ConnectionString = "Data Source=Bank.db;Version=3;";
        private static SQLiteCommand command;
        public BankEmployeePage(string userName)
        {
            InitializeComponent();
            this.Text = userName;
            ConnaectToServer();
        }

        private void ConnaectToServer()
        {
            client = new TcpClient("127.0.0.1", 9000);
            clientThread = new Thread(new ThreadStart(ListenForData));
            clientThread.Start();
        }

        private void ListenForData()
        {
            while (true)
            {
                try
                {
                    byte[] bytes = new byte[1024];
                    NetworkStream stream = client.GetStream();

                    int byteRead = stream.Read(bytes, 0, bytes.Length);
                    string receiveData = Encoding.UTF8.GetString(bytes, 0, byteRead);

                    UpdateListBoxes(receiveData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error {ex.Message}");
                }
            }
        }

        private void UpdateListBoxes(string data)
        {
            if (listBox1.InvokeRequired || listBox3.InvokeRequired || listBox2.InvokeRequired)
            {
                listBox1.Invoke(new Action(() => UpdateListBoxes(data)));
                listBox3.Invoke(new Action(() => UpdateListBoxes(data)));
                listBox2.Invoke(new Action(() => UpdateListBoxes(data)));
            }
            else
            {
                string[] tablesData = data.Split(new[] { "Transactions:", "UserProfile:", "Request:" }, StringSplitOptions.RemoveEmptyEntries);

                if (tablesData.Length >= 3)
                {
                    listBox1.Items.Clear();
                    listBox1.Items.AddRange(tablesData[0].Split('\n'));

                    listBox3.Items.Clear();
                    listBox3.Items.AddRange(tablesData[1].Split('\n'));

                    listBox2.Items.Clear();
                    listBox2.Items.AddRange(tablesData[2].Split('\n'));
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            RequestDataFromServer();
        }


        private void RequestDataFromServer()
        {
            try
            {
                byte[] requestData = Encoding.UTF8.GetBytes("RefreshData");
                client.GetStream().Write(requestData, 0, requestData.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}");
            }
        }

        private void BankEmployeePage_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO User (username, password, account_balance) VALUES (@username, @password, @account_balance)";
                    string usrName = textBox6.Text;
                    string pass = textBox5.Text;

                    try
                    {
                        using (command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@username", usrName);
                            command.Parameters.AddWithValue("@password", pass);
                            command.Parameters.AddWithValue("@account_balance", 1);
                            command.ExecuteNonQuery();
                            MessageBox.Show($"Пользователь создан со следующими папреметрами: \n username{usrName}\n Пароль: {pass}\n, уведомите пользователя по его электронной почте указанной в анкете");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось создать пользователя:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось подключиться к БД:{ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string usrNmae = textBox1.Text;
            string infoAboutTransaction = textBox3.Text;

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    try
                    {
                        string queryGetEmployeID = "select employee_id from BankEmployee where username = @username";
                        using (command = new SQLiteCommand(queryGetEmployeID, connection))
                        {
                            command.Parameters.AddWithValue("@username", this.Text);
                            object result = command.ExecuteScalar();
                            int EmploID = Convert.ToInt32(result);
                            try
                            {
                                string queryGetUserID = "select user_id from User where username = @userName";
                                using (command = new SQLiteCommand(queryGetUserID, connection))
                                {
                                    command.Parameters.AddWithValue("@userName", usrNmae);
                                    object rerUsrId = command.ExecuteScalar();
                                    int UserID = Convert.ToInt32(rerUsrId);

                                    try
                                    {
                                        string query = "INSERT INTO AccountReport (employee_id, name_of_user, user_id, TransactionInfo) VALUES (@employee_id, @name_of_user, @user_id, @TransactionInfo)";
                                        using (command = new SQLiteCommand(query, connection))
                                        {
                                            command.Parameters.AddWithValue("@employee_id", EmploID);
                                            command.Parameters.AddWithValue("@name_of_user", usrNmae);
                                            command.Parameters.AddWithValue("@user_id", UserID);
                                            command.Parameters.AddWithValue("@TransactionInfo", infoAboutTransaction);
                                            command.ExecuteNonQuery();
                                            MessageBox.Show($"Отчет о переводе был создан, была передана следующаия информация:\n" +
                                                $"ID сотрудника банка,который делела очет: {EmploID}\n" + $"Username запрашивающего: {usrNmae}\n" +
                                                $"ID пользователя: {UserID}" +
                                                $"Информация о транзакции: {infoAboutTransaction}");
                                        }


                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show($"Не удалось отправть данные о транзакции: {ex.Message}");
                                    }

                                }


                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Не удолось получить ID из USER:{ex.Message}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось получить ID: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось подключиться к БД:{ex.Message}");
            }
        }

       
    }
}
