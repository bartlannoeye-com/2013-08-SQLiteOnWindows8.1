using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SQLiteWinRT;

namespace SQLiteOnWin8._1Preview
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string DatabaseName = "MyDatabase.db";

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void CreateDatabase_OnClick(object sender, RoutedEventArgs e)
        {
            // output path to console
            var installedLocation = ApplicationData.Current.LocalFolder;
            Debug.WriteLine(installedLocation.Path);

            var db = await CreateAndOpenDatabase();

            string query = "CREATE TABLE PEOPLE " +
                   "(Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                   "Name varchar(100), " +
                   "Surname varchar(100))";

            await db.ExecuteStatementAsync(query);
        }

        private async Task<Database> CreateAndOpenDatabase()
        {
            var db = new Database(ApplicationData.Current.LocalFolder, DatabaseName);
            try
            {
                await db.OpenAsync();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                var result = Database.GetSqliteErrorCode(ex.HResult);
                throw new Exception("Failed to create database " + result);
            }

            return db;
        }
    }
}
