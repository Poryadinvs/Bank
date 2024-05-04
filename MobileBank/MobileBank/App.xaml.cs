using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Runtime.InteropServices;

namespace MobileBank
{
    public partial class App : Application
    {
        private static DB db;
        public const string dbName = "Bank.db";
        public static DB Db
        {
            get
            {
                if (db == null)
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName);

                    if (!File.Exists(path))
                    {
                        var assambly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                        using (Stream stream = assambly.GetManifestResourceStream($"MobileBank.{dbName}"))
                        {
                            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                            {
                                stream.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    }
                   db = new DB(path);
                }
                return db;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
