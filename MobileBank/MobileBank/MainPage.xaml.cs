using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data.SQLite;
using System.IO;
using System.Runtime.InteropServices;

namespace MobileBank
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            ShowUser();
        }

        protected override void OnAppearing()
        {
            ShowUser();
        }

        private void ShowUser()
        {
            Test.ItemsSource = App.Db.GetUserProfile();
        }

        private async void Enter_Clicked(object sender, EventArgs e)
        {
            string Login = login.Text;
            string Password = password.Text;
            var selectedRole = (string)RoplePicker.SelectedItem;
            List<User> users = App.Db.GetUser();
            List<BankEmployee> bankEmployees = App.Db.GetBankEmployee();

            User user = users.FirstOrDefault(u => u.username == Login && u.password == Password);
            BankEmployee bankEmployee = bankEmployees.FirstOrDefault(u => u.username == Login && u.password == Password);


           switch (selectedRole)
           {
                case "User":
                    if(user != null)
                    {
                        MobileUserPage mobileUserPage = new MobileUserPage(user.username);
                        Application.Current.MainPage = mobileUserPage;
                    }
                    else
                    {
                        await DisplayAlert("Error", "Неверный пароиль или логин", "OK");
                    }
                    break;

                case "BankEmployee":
                    if (bankEmployee != null)
                    {
                        MobileBankEmployeePage mobileBankPage = new MobileBankEmployeePage(bankEmployee.username);
                        Application.Current.MainPage = mobileBankPage;
                    }
                    else
                    {
                        await DisplayAlert("Error", "Неверный пароиль или логин", "OK");
                    }
                    break;
                default:
                    await DisplayAlert("Error", "Прежде чем войти, выберите роль, если у вас не аккаунта, нажмите кнопку \"Зарегистрироваться\"", "OK");
                    break;
           }
        }

        private async void Registration_Clicked(object sender, EventArgs e)
        {
            var selectedRole = (string)RoplePicker.SelectedItem;

            switch (selectedRole)
            {
                case "User":
                    MobileUserProfile mobileUserProfile = new MobileUserProfile();
                    Application.Current.MainPage = mobileUserProfile;
                    break;

                case "BankEmployee":
                    await DisplayAlert("Warinig", "Для приема на работу, обратитесь к нашему HR менеджреу по телефону +1 999666999666, или заполните заявку на сайте www.BestBankYouvEvereSeen.com", "Ok");
                    break;
                default:
                    await DisplayAlert("Error", "Прежде чем войти, выберите роль, если у вас не аккаунта, нажмите кнопку \"Зарегистрироваться\"", "OK");
                    break;
            }
        }

        private void RemoveUserProfile_Clicked(object sender, EventArgs e)
        {
            App.Db.DeleteAllFromUserProfile();
            ShowUser();
        }

        private async void DropAccountReport_Clicked(object sender, EventArgs e)
        {
            App.Db.DropTableAccountReport();
            await DisplayAlert("Ok", "ТАблица удалена", "OK");
        }
    }
}