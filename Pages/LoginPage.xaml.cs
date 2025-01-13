using KKDK_Parcel_Delivery.Models;
using KKDK_Parcel_Delivery.Services;
using KKDK_Parcel_Delivery.Pages.StudentPages;  // For StudentHomePage
using KKDK_Parcel_Delivery.Pages.AdminPages;    // For AdminHomePage
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;


namespace KKDK_Parcel_Delivery.Pages
{
    public partial class LoginPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public LoginPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string matricNum = MatricNumberEntry.Text;
            string password = PasswordEntry.Text;

            // Check if the credentials match a student
            var students = await _databaseService.GetStudentsAsync();
            var loggedInStudent = students.FirstOrDefault(s => s.MatricNum == matricNum && s.Password == password);

            // Check if the credentials match an admin
            var admins = await _databaseService.GetAdminsAsync();
            var loggedInAdmin = admins.FirstOrDefault(a => a.MatricNum == matricNum && a.Password == password);

            if (loggedInStudent != null)
            {
                // Login successful for student
                // Store the student's matric number in Preferences
                Preferences.Set("StudentMatricNumber", loggedInStudent.MatricNum);

                // Navigate to the student's home page
                await Navigation.PushAsync(new StudentHomePage());
            }
            else if (loggedInAdmin != null)
            {
                // Login successful for admin
                // Navigate to the admin's home page
                await Navigation.PushAsync(new AdminHomePage());
            }
            else
            {
                // Login failed
                await DisplayAlert("Error", "Invalid matric number or password.", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Navigate to the registration page
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
