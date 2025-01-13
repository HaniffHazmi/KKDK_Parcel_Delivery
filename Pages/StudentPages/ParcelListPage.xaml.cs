using KKDK_Parcel_Delivery.Models;
using KKDK_Parcel_Delivery.Services;
using System;
using Microsoft.Maui.Storage; // Use MAUI storage for preferences

namespace KKDK_Parcel_Delivery.Pages.StudentPages
{
    public partial class ParcelListPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public ParcelListPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        // Method to load the student parcels
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve the logged-in student's matric number from Preferences
            string matricNumber = Preferences.Get("StudentMatricNumber", string.Empty);

            if (string.IsNullOrEmpty(matricNumber))
            {
                // No student logged in, prompt to log in again
                await DisplayAlert("Error", "Student not logged in", "OK");
                await Navigation.PushAsync(new LoginPage()); // Navigate to the login page
                return;
            }

            // Fetch student details from the database
            var student = await _databaseService.GetStudentByMatricNumAsync(matricNumber);

            if (student != null)
            {
                // Fetch the parcels associated with the student
                var parcels = await _databaseService.GetParcelsForStudentAsync(student.StudentId);
                ParcelListView.ItemsSource = parcels;
            }
            else
            {
                // Handle case where student is not found in the database
                await DisplayAlert("Error", "Student not found", "OK");
                await Navigation.PushAsync(new LoginPage()); // Redirect to login if student not found
            }
        }


        // Navigate to AddParcelPage when the button is clicked
        private async void OnAddParcelClicked(object sender, EventArgs e)
        {
            // Retrieve the logged-in student's matric number from Preferences
            string matricNumber = Preferences.Get("StudentMatricNumber", string.Empty); // Adjust based on how you store the matric number

            if (string.IsNullOrEmpty(matricNumber))
            {
                await DisplayAlert("Error", "Student not logged in", "OK");
                return;
            }

            var student = await _databaseService.GetStudentByMatricNumAsync(matricNumber);

            if (student != null)
            {
                // Pass the student's matric number to AddParcelPage
                await Navigation.PushAsync(new AddParcelPage(student.MatricNum));
            }
            else
            {
                await DisplayAlert("Error", "Student not found. Please try again.", "OK");
            }
        }

        // Handle parcel item tap
        private async void OnParcelTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var parcel = e.Item as Parcel;

                // Navigate to the parcel detail page, passing the parcel object to the detail page
                await Navigation.PushAsync(new ParcelDetailPage(parcel.ParcelId));  // Pass ParcelId or any other relevant data
            }
        }
    }
}
