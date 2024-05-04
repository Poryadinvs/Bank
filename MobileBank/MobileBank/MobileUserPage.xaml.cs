using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileBank
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MobileUserPage : ContentPage
    {
        public MobileUserPage(string CurrentUserName)
        {
            InitializeComponent();
            Balance.Text = App.Db.GetUserBalance(CurrentUserName).ToString();
            Title = App.Db.GetUserName(CurrentUserName);
        }

        //protected override void OnAppearing()
        //{
        //    ShowTransaction();
        //    ShowRequest();
        //}

        //private void ShowTransaction()
        //{
        //    Test.ItemsSource = App.Db.GetTransaction();
        //}

        //private void ShowRequest()
        //{
        //    TestTwo.ItemsSource = App.Db.GetRequest();
        //}

        private async void SendMoney_Clicked(object sender, EventArgs e)
        {
            decimal amount = Convert.ToDecimal(Amount.Text);
            decimal balance = Convert.ToDecimal(Balance.Text);
            string recipientUsername = RecipientsUsername.Text;

            if (balance < amount)
            {
                await DisplayAlert("Warning", "Недостаточно средств", "OK");
            }
            else
            {
                string senderUsername = Title;

                try
                {
                    App.Db.SendMoney(senderUsername, recipientUsername, amount);
                    await DisplayAlert("Success", $"Средства успешно отправлены пользователю {recipientUsername}", "OK");

                    Balance.Text = App.Db.GetUserBalance(senderUsername).ToString();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        private void RequestReport_Clicked(object sender, EventArgs e)
        {
            string senderUserName = Title;
            int senderId = App.Db.GetID(senderUserName);
            Request req = new Request {
                sender_id = senderId,
                sender_name = senderUserName,
            };
            App.Db.SaveRequest(req);
            //ShowRequest();
        }

        private void AllRequests_Clicked(object sender, EventArgs e)
        {
            string senderUserName = Title;
            GetExecAccuntReport.ItemsSource = App.Db.GetExecAccuntReport(senderUserName);
        }
    }
}