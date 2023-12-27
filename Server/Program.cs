using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Data.SQLite;

TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9000);
server.Start();

while (true)
{
    Console.WriteLine("Waiting for a client to connect...");

    TcpClient client = server.AcceptTcpClient();
    Console.WriteLine("Client connected!");

    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
    clientThread.Start(client);
}

static void HandleClient(object obj)
{
    TcpClient tcpClient = (TcpClient)obj;

    NetworkStream stream = tcpClient.GetStream();

    StringBuilder dataToSend = new StringBuilder();

    dataToSend.AppendLine("Transactions:");
    dataToSend.AppendLine(GetTableData("Transactions"));

    dataToSend.AppendLine("UserProfile:");
    dataToSend.AppendLine(GetTableData("UserProfile"));

    byte[] data = Encoding.UTF8.GetBytes(dataToSend.ToString());
    stream.Write(data, 0, data.Length);

    tcpClient.Close();
}

static string GetTableData(string tableName)
{
    SQLiteConnection dbConnection = new SQLiteConnection("Data Source=D:\\Влад\\ДОМАХА\\Учебная практика 3 курс\\Банк\\Bank'\\User\\bin\\Debug\\net7.0-windows\\Bank.db;Version=3;");
    dbConnection.Open();

    SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {tableName}", dbConnection);
    SQLiteDataReader reader = command.ExecuteReader();

    StringBuilder tableData = new StringBuilder();

    while (reader.Read())
    {
        tableData.AppendLine($"{reader[0]}, {reader[1]}, {reader[2]}, {reader[3]}, {reader[4]}");
    }

    reader.Close();
    dbConnection.Close();

    return tableData.ToString();
}