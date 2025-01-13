using KKDK_Parcel_Delivery.Models;
using KKDK_Parcel_Delivery.Services;
using System;

namespace KKDK_Parcel_Delivery.Pages.StudentPages
{
    public partial class AddParcelPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly string _studentMatricNum; // Matric number of the logged-in student

        // Modify constructor to accept the matric number
        public AddParcelPage(string studentMatricNum)
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            _studentMatricNum = studentMatricNum; // Store the matric number
        }

        // Handle the parcel add button click
        private async void OnAddParcelClicked(object sender, EventArgs e)
        {
            string trackingNumber = TrackingNumberEntry.Text;
            string courier = CourierPicker.SelectedItem.ToString();
            string receiverName = ReceiverNameEntry.Text;
            DateTime dateArrived = DateArrivedPicker.Date;

            if (string.IsNullOrEmpty(trackingNumber) || string.IsNullOrEmpty(receiverName))
            {
                await DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            // Fetch the student based on the matric number
            var student = await _databaseService.GetStudentByMatricNumAsync(_studentMatricNum);

            // Check if student exists
            if (student == null)
            {
                await DisplayAlert("Error", "Student not found. Please try again.", "OK");
                return;
            }

            // Create new parcel
            var newParcel = new Parcel
            {
                TrackingNumber = trackingNumber,
                Courier = (Courier)Enum.Parse(typeof(Courier), courier),
                ReceiverName = receiverName,
                DateArrived = dateArrived,
                StudentId = student.StudentId
            };

            // Insert parcel into database
            await _databaseService.InsertParcelAsync(newParcel);
            await DisplayAlert("Success", "Parcel added successfully", "OK");

            // Navigate back to the parcel list
            await Navigation.PopAsync();
        }
    }
}
