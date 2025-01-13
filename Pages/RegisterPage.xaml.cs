using KKDK_Parcel_Delivery.Models;
using KKDK_Parcel_Delivery.Services;

namespace KKDK_Parcel_Delivery.Pages
{
    public partial class RegisterPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public RegisterPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string matricNum = MatricNumEntry.Text;
            string name = NameEntry.Text;
            string password = PasswordEntry.Text;
            string phoneNum = PhoneNumEntry.Text;
            string college = CollegePicker.SelectedItem.ToString();
            string block = BlockPicker.SelectedItem.ToString();
            string roomNo = RoomNoEntry.Text;

            if (string.IsNullOrEmpty(matricNum) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(phoneNum) || string.IsNullOrEmpty(college) || string.IsNullOrEmpty(block) ||
                string.IsNullOrEmpty(roomNo))
            {
                await DisplayAlert("Error", "Please fill in all the fields.", "OK");
                return;
            }

            // Create a new student object
            var newStudent = new Student
            {
                MatricNum = matricNum,
                StudentName = name,
                Password = password,
                PhoneNum = phoneNum,
                College = (College)Enum.Parse(typeof(College), college),
                Block = (Block)Enum.Parse(typeof(Block), block),
                RoomNo = roomNo
            };

            // Insert the new student into the database
            await _databaseService.InsertStudentAsync(newStudent);

            // Show success message
            await DisplayAlert("Success", "Registration successful.", "OK");

            // Navigate back to login page after successful registration
            await Navigation.PopAsync();
        }
    }
}
