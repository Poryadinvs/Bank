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

namespace User
{
    public partial class UserProfile : Form
    {
        private const string ConnectionString = "Data Source=Bank.db;Version=3;";
        private static SQLiteCommand command;

        public UserProfile()
        {
            InitializeComponent();
        }

        private void UserProfile_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    int ID = GetID();
                    string RegDate = DateTime.Now.ToString();
                    string FName = textBox1.Text;
                    string LName = textBox2.Text;
                    string SName = textBox3.Text;
                    string EmploymentType = textBox4.Text;
                    string Adres = textBox5.Text;
                    string DateOfBirth = textBox6.Text;
                    string Email = textBox7.Text;

                    string query = "INSERT INTO UserProfile (user_id, first_name, last_name, sec_name, birth_date, address, email, registration_date, employment_type) VALUES " +
                        "(@user_id, @first_name, @last_name, @sec_name, @birth_date, @address, @email, @registration_date, @employment_type)";

                   try
                   {
                        using (command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@user_id", ID);
                            command.Parameters.AddWithValue("@first_name", FName);
                            command.Parameters.AddWithValue("@last_name", LName);
                            command.Parameters.AddWithValue("@sec_name", SName);
                            command.Parameters.AddWithValue("@birth_date", DateOfBirth);
                            command.Parameters.AddWithValue("@address", Adres);
                            command.Parameters.AddWithValue("@email", Email);
                            command.Parameters.AddWithValue("@registration_date", RegDate);
                            command.Parameters.AddWithValue("@employment_type", EmploymentType);
                            command.ExecuteNonQuery();

                            MessageBox.Show($"Запрос на создание профиля было отправлен\n Отправеднные данные:\n" +
                                $"Ваш будущий ID: {ID}\n" +
                                $"Ваше ФИО: {FName}, {LName}, {SName}\n" +
                                $"Ваша дата рождления: {DateOfBirth}" +
                                $"Ваш адрес: {Adres}\n" +
                                $"Ваша электронная почта: {Email}\n" +
                                $"Дата регистрации: {RegDate}\n" +
                                $"Ваш тип занятости: {EmploymentType}\n");
                        }
                   }
                   catch(Exception ex)
                   {
                        MessageBox.Show($"Не получилось отправить запрос на создание аккаунта: {ex.Message}");
                   }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не получилось подключиться к базе данных: {ex.Message}");
            }
        }

        private int GetID()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "Select count(user_id) from User";
                using (command = new SQLiteCommand(query, connection))
                {
                    object res = command.ExecuteScalar();
                    int countOfUsers = Convert.ToInt32(res);
                    MessageBox.Show($"Всего строк в таблицке:{countOfUsers}");
                    return countOfUsers+1;
                }

            }

        }

    }
}
