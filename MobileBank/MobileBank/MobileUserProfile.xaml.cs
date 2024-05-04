using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileBank
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MobileUserProfile : ContentPage
    {
        public MobileUserProfile()
        {
            InitializeComponent();
            //ConnectToServer();
        }

        protected override void OnAppearing()
        {
            ShowUser();
        }
        private void ShowUser()
        {
            Test.ItemsSource = App.Db.GetUserProfile();
        }
        //private TcpClient client;

        //async void ConnectToServer()
        //{
        //    try
        //    {
        //        client = new TcpClient();
        //        await client.ConnectAsync("10.0.2.16", 9000);
        //        await DisplayAlert("Connected", "Connected to server!", "Ok");
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Error", $"Error connecting to server ${ex.Message}", "Ok");
        //    }
        //}

        //async void SendMessageToServer(string message)
        //{
        //    try
        //    {
        //        if (client != null && client.Connected)
        //        {
        //            NetworkStream stream = client.GetStream();
        //            StreamWriter writer = new StreamWriter(stream);
        //            await writer.WriteLineAsync(message);
        //            await writer.FlushAsync();
        //        }
        //        else
        //        {
        //            await DisplayAlert("Error", "Not connected to server.", "Ok");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Error", $"Error sending message to server: {ex.Message}", "Ok");
        //    }
        //}

        private void SendRequest_Clicked(object sender, EventArgs e)
        {
            int ID = User.GetNewId();
            string name = Name.Text.Trim();
            string secName = SecondName.Text.Trim();
            string lastName = LastName.Text.Trim();
            string typeOfEmployment = TypeOfEmployment.Text.Trim();
            string eMail = EMail.Text.Trim();
            string adress = Adres.Text.Trim();
            DateTime RegDate = DateTime.Now;
            DateTime dateOfBirth = DateOfBirth.Date;

            UserProfile userProfile = new UserProfile
            {
                user_id = ID,
                first_name = name,
                sec_name = secName,
                last_name = lastName,
                birth_date = dateOfBirth,
                address = adress,
                email = eMail,
                registration_date = RegDate,
                employment_type = typeOfEmployment
            };

            App.Db.SaveUserProfile(userProfile);
            ShowUser();

            //string query = $"INSERT INTO UserProfile (user_id, first_name, sec_name, last_name, birth_date, address, email, registration_date, employment_type) " +
            //               $"VALUES ({ID}, '{name}', '{secName}', '{lastName}', '{dateOfBirth}', '{adress}', '{eMail}', '{RegDate}', '{typeOfEmployment}')";
            //SendMessageToServer(query);

            Name.Text = "";
            SecondName.Text = "";
            LastName.Text = "";
            TypeOfEmployment.Text = "";
            EMail.Text = "";
            Adres.Text = "";
            DateOfBirth.Date = DateTime.Now;
        }



    }
}