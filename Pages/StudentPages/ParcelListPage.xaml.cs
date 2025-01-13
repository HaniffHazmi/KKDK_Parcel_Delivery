using KKDK_Parcel_Delivery.Models;
using KKDK_Parcel_Delivery.Services;
using System.Linq;

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

            // Get the current logged-in student (you can replace this with actual authentication logic)
            var student = await _databaseService.GetStudentByMatricNumAsync("student_matric_number"); // Replace with the logged-in student matric number

            if (student != null)
            {
                // Fetch the parcels associated with the student
                var parcels = await _databaseService.GetParcelsForStudentAsync(student.StudentId);
                ParcelListView.ItemsSource = parcels;
            }
            else
            {
                await DisplayAlert("Error", "Student not found", "OK");
            }
        }

        // Navigate to AddParcelPage when the button is clicked
        private async void OnAddParcelClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddParcelPage());
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
