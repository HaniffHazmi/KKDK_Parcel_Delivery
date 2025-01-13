using KKDK_Parcel_Delivery.Models;
using KKDK_Parcel_Delivery.Services;
using System;

namespace KKDK_Parcel_Delivery.Pages.StudentPages
{
    public partial class AddParcelPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public AddParcelPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
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

            var student = await _databaseService.GetStudentByMatricNumAsync("student_matric_number"); // Replace with logged-in student's matric number

            var newParcel = new Parcel
            {
                TrackingNumber = trackingNumber,
                Courier = (Courier)Enum.Parse(typeof(Courier), courier),
                ReceiverName = receiverName,
                DateArrived = dateArrived,
                StudentId = student.StudentId
            };

            await _databaseService.InsertParcelAsync(newParcel);
            await DisplayAlert("Success", "Parcel added successfully", "OK");

            // Navigate back to the parcel list
            await Navigation.PopAsync();
        }
    }
}
