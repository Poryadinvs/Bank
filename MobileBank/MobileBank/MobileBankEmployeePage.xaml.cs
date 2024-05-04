using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileBank
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MobileBankEmployeePage : ContentPage
    {
        public MobileBankEmployeePage(string currentusername)
        {
            InitializeComponent();
            Title = App.Db.GetBankEmployeeUsername(currentusername);
        }

        protected override async void OnAppearing()
        {
            ShowRequests();
            ShowTransations();
            ShowUserProfileRewuests();
            await DisplayAlert("Wellcome", $"Допро прожаловть {Title}", "Ok");
        }

        private void ShowTransations()
        {
            transactins.ItemsSource = App.Db.GetTransaction();
        }

        private void ShowRequests()
        {
            Requests.ItemsSource = App.Db.GetRequest();
        }

        private void ShowUserProfileRewuests()
        {
            UserProfileRequest.ItemsSource = App.Db.GetAccountReport();
        }

        private void UpdateData_Clicked(object sender, EventArgs e)
        {
            ShowRequests();
            ShowTransations();
            ShowUserProfileRewuests();
        }

        private void MakeUserProfile_Clicked(object sender, EventArgs e)
        {
            string usrname = UsernameForUser.Text;
            string password = passwordForUser.Text;

            User user = new User { 
                username = usrname,
                password = password,
                account_balance = 0
            };

            App.Db.SaveUser(user);
            UsernameForUser.Text = "";
            passwordForUser.Text = ""; 
        }

        private async void MakeTransactionREport_Clicked(object sender, EventArgs e)
        {
            string empUsername = Title;
            int EmploID = App.Db.GetBankEmployeeID(empUsername);
            string infoAboutTransaction = Information.Text;
            string usrNmae = UsersUsername.Text; 
            int UserID = App.Db.GetID(usrNmae);

            AccountReport accountReport = new AccountReport
            {
                employee_id = EmploID,
                name_of_user = usrNmae,
                user_id = UserID,
                TransactionInfo = infoAboutTransaction
            };

            App.Db.SaveAccountReport(accountReport);

            await DisplayAlert("Success", $"Отчет о переводе был создан, была передана следующаия информация:\n" +
                                                $"ID сотрудника банка,который делела очет: {EmploID}\n" + $"Username запрашивающего: {usrNmae}\n" +
                                                $"ID пользователя: {UserID}" +
                                                $"Информация о транзакции: {infoAboutTransaction}", "Ok");


        }
    }
}