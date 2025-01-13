using KKDK_Parcel_Delivery.Models;
using KKDK_Parcel_Delivery.Services;
using System;
using Microsoft.Maui.Controls;

namespace KKDK_Parcel_Delivery.Pages.StudentPages
{

	public partial class StudentProfilePage : ContentPage
	{
        private readonly DatabaseService _databaseService;
        private Student _student;

        public StudentProfilePage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        // Load the student data when the page appears
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            string matricNumber = Preferences.Get("StudentMatricNumber", string.Empty);
            if (!string.IsNullOrEmpty(matricNumber))
            {
                _student = await _databaseService.GetStudentByMatricNumAsync(matricNumber);

                if (_student != null)
                {
                    // Display student info
                    MatricNumberEntry.Text = _student.MatricNum;
                    FullNameEntry.Text = _student.StudentName;
                    EmailEntry.Text = _student.Email;
                    PhoneNumberEntry.Text = _student.PhoneNum;
                }
                else
                {
                    await DisplayAlert("Error", "Student not found", "OK");
                }
            }
        }

        // Enable editing of student information
        private void OnEditClicked(object sender, EventArgs e)
        {
            FullNameEntry.IsEnabled = true;
            EmailEntry.IsEnabled = true;
            PhoneNumberEntry.IsEnabled = true;

            // Show Save button, hide Edit button
            EditButton.IsVisible = false;
            SaveButton.IsVisible = true;
        }

        // Save the updated student information
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _student.StudentName = FullNameEntry.Text;
            _student.Email = EmailEntry.Text;
            _student.PhoneNum = PhoneNumberEntry.Text;

            await _databaseService.UpdateStudentAsync(_student);

            await DisplayAlert("Success", "Profile updated successfully", "OK");

            // Disable editing and switch buttons back
            FullNameEntry.IsEnabled = false;
            EmailEntry.IsEnabled = false;
            PhoneNumberEntry.IsEnabled = false;

            EditButton.IsVisible = true;
            SaveButton.IsVisible = false;
        }
    }
}