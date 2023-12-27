using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Data.SQLite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bank_
{
    public partial class Form1 : Form
    {
        TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9000);


        public Form1()
        {
            InitializeComponent();
            server.Start();

            while (true)
            {
                label1.Text = "Waiting for a client to connect...";
                MessageBox.Show("Ожидаю подключения");
                TcpClient client = server.AcceptTcpClient();
                label1.Text = "Client connected!";
                MessageBox.Show("Клиент подключен");

                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                clientThread.Start(client);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }
        private static void HandleClient(object obj)
        {
            TcpClient tcpClient = (TcpClient)obj;

            NetworkStream stream = tcpClient.GetStream();

            StringBuilder dataToSend = new StringBuilder();

            dataToSend.AppendLine("Transactions");
            dataToSend.AppendLine(GetTableDate("Transactions"));

            dataToSend.AppendLine("UserProfile");
            dataToSend.AppendLine(GetTableDate("UserProfile"));

            byte[] data = Encoding.UTF8.GetBytes(dataToSend.ToString());
            stream.Write(data, 0, data.Length);

            tcpClient.Close();
        }
        private static string GetTableDate(string tableName)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=Bank.db;Version=3;");
            connection.Open();

            SQLiteCommand command = new SQLiteCommand($"Select * from {tableName}", connection);
            SQLiteDataReader reader = command.ExecuteReader();

            StringBuilder tableData = new StringBuilder();
            while (reader.Read())
            {
                tableData.AppendLine($"{reader[0]}, {reader[1]}, {reader[2]}, {reader[3]}, {reader[4]}");
            }

            reader.Close();
            connection.Close();
            return tableData.ToString();
        }
    }
}