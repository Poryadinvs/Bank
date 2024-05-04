using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Data.SQLite;

TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9000);
server.Start();
List<TcpClient> count = new List<TcpClient>();
int maxConnected = 5;
while (true)
{
    Console.WriteLine("Waiting for a client to connect...");

    if (count.Count <= maxConnected)
    {
        TcpClient client = await server.AcceptTcpClientAsync();
        Console.WriteLine("Client connected!");
        count.Add(client);
        Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
        clientThread.Start(client);
    }
    else { Console.WriteLine("Сервер перегружен, подключитесь позже");
        server.Stop();
    }
}

static void HandleClient(object obj)
{
    TcpClient tcpClient = (TcpClient)obj;

    NetworkStream stream = tcpClient.GetStream();
    StreamReader reader = new StreamReader(stream);
    while (true)
    {
        StringBuilder dataToSend = new StringBuilder();

        dataToSend.AppendLine("Transactions:");
        dataToSend.AppendLine(GetTableData("Transactions"));

        dataToSend.AppendLine("UserProfile:");
        dataToSend.AppendLine(GetTableData("UserProfile"));

        dataToSend.AppendLine("Request:");
        dataToSend.AppendLine(GetTableData("Request"));

        dataToSend.AppendLine("Request:");
        dataToSend.AppendLine(GetTableData("Request"));


        byte[] data = Encoding.UTF8.GetBytes(dataToSend.ToString());
        stream.Write(data, 0, data.Length);

        //string query = reader.ReadLine();
        //if (!string.IsNullOrEmpty(query))
        //{
        //    Console.WriteLine($"Received query: {query}");
        //    ExecuteQuery(query);
        //}

        Thread.Sleep(5000);
    }
}

//static void ExecuteQuery(string query)
//{
//    try
//    {
//        SQLiteConnection dbConnection = new SQLiteConnection("Data Source=C:\\Vlad\\Bank\\MobileBank\\MobileBank\\Bank.db;Version=3;");
//        dbConnection.Open();

//        SQLiteCommand command = new SQLiteCommand(query, dbConnection);
//        int rowsAffected = command.ExecuteNonQuery();
//        Console.WriteLine($"Rows affected: {rowsAffected}");

//        dbConnection.Close();
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Error executing query: {ex.Message}");
//    }
//}

static string GetTableData(string tableName)
{
    SQLiteConnection dbConnection = new SQLiteConnection("Data Source=C:\\Vlad\\Bank\\MobileBank\\MobileBank\\Bank.db;Version=3;");
    dbConnection.Open();

    SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {tableName}", dbConnection);
    SQLiteDataReader reader = command.ExecuteReader();

    StringBuilder tableData = new StringBuilder();

    while (reader.Read())
    {
        StringBuilder rowData = new StringBuilder();
        for (int i = 0; i < reader.FieldCount; i++)
        {
            rowData.Append($"{reader[i]}, ");
        }
        tableData.AppendLine(rowData.ToString().TrimEnd(',', ' '));
    }

    reader.Close();
    dbConnection.Close();

    return tableData.ToString();
}