using KKDK_Parcel_Delivery.Pages;
using KKDK_Parcel_Delivery.Models;
using KKDK_Parcel_Delivery.Services;
using Microsoft.Maui.Controls;

namespace KKDK_Parcel_Delivery
{
    public partial class App : Application
    {
        private readonly DatabaseService _databaseService;

        public App()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();

            // Ensure that the admin exists in the database
            InitializeAdmin();

            // Set the first page to be the LoginPage wrapped in a NavigationPage
            MainPage = new NavigationPage(new LoginPage());
        }

        private async void InitializeAdmin()
        {
            // Check if admin exists
            var admins = await _databaseService.GetAdminsAsync();

            if (!admins.Any())
            {
                // Admin does not exist, create a default admin
                var defaultAdmin = new Admin
                {
                    AdminName = "Admin",
                    MatricNum = "ADMIN01",
                    Password = "ADMIN",  // Set your password
                    PhoneNum = "123456789"
                };

                // Insert default admin into the database
                await _databaseService.InsertAdminAsync(defaultAdmin);
            }
        }
    }
}
