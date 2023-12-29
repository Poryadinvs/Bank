using System.Data;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;

namespace User
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const string ConnectionString = "Data Source=Bank.db;Version=3;";
        private static SQLiteCommand command;


        private void button1_Click(object sender, EventArgs e)
        {
            string selectRole = comboBox1.SelectedItem.ToString();
            string username = textBox1.Text;
            string password = textBox2.Text;
            switch (selectRole)
            {
                case "User":

                    if (CheckUser(username, password))
                    {
                        UserPage UP = new UserPage(username);
                        UP.Show();
                        this.Hide();
                    }
                    else
                        MessageBox.Show("Неверный логин или пароль");
                    break;

                case "BankEmployee":

                    if (CheckBankEmployee(username, password))
                    {
                        BankEmployeePage BEP = new BankEmployeePage(username);
                        BEP.Show();
                        this.Hide();
                    }
                    else
                        MessageBox.Show("Неверный логин или пароль");
                    break;

                case "Manager":

                    if (CheckManager(username, password))
                    {
                        ManagerPage MP = new ManagerPage();
                        MP.Show();
                        this.Hide();
                    }
                    else
                        MessageBox.Show("Неверный логин или пароль");
                    break;

                case "Administrator":

                    if (CheckAdministrator(username, password))
                    {
                        AdministratorPage AP = new AdministratorPage();
                        AP.Show();
                        this.Hide();
                    }
                    else
                        MessageBox.Show("Неверный логин или пароль");
                    break;

                default:
                    MessageBox.Show("Выберите тип пользователя");
                    break;

            };


        }

        private bool CheckManager(string username, string password)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Manager WHERE username = @username AND password = @password";
                    using (command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        connection.Close();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке логина и пароля: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool CheckAdministrator(string username, string password)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Administrator WHERE username = @username AND password = @password";
                    using (command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        connection.Close();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке логина и пароля: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool CheckUser(string username, string password)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM User WHERE username = @username AND password = @password";
                    using (command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        connection.Close();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке логина и пароля: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool CheckBankEmployee(string username, string password)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM BankEmployee WHERE username = @username AND password = @password";
                    using (command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        connection.Close();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке логина и пароля: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserProfile UP = new UserProfile();

            string selectRole = comboBox1.SelectedItem.ToString();
            switch (selectRole)
            {
                case "User":

                    UP.Show();
                    this.Hide();
                    break;

                case "BankEmployee":
                    MessageBox.Show("Для приема на работу, обратитесь к нашему HR менеджреу по телефону +1 999666999666, или заполните заявку на сайте www.BestBankYouvEvereSeen.com");
                    break;

                case "Manager":
                    MessageBox.Show("Для приема на работу, обратитесь к нашему HR менеджреу по телефону +1 999666999666, или заполните заявку на сайте www.BestBankYouvEvereSeen.com");
                    break;

                case "Administrator":
                    MessageBox.Show("Для приема на работу, обратитесь к нашему HR менеджреу по телефону +1 999666999666, или заполните заявку на сайте www.BestBankYouvEvereSeen.com");
                    break;

                default:
                    MessageBox.Show("Выберите тип пользователя");
                    break;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}