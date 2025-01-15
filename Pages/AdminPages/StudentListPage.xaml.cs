using KKDK_Parcel_Delivery.Models;
using KKDK_Parcel_Delivery.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KKDK_Parcel_Delivery.Pages.AdminPages
{
    public partial class StudentListPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<Student> _students;

        public StudentListPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            _students = new ObservableCollection<Student>();
            StudentCollectionView.ItemsSource = _students;
            LoadStudentsAsync(); // Load students when the page loads
        }

        // Load all students from the database
        private async void LoadStudentsAsync()
        {
            var students = await _databaseService.GetStudentsAsync();
            _students.Clear();
            foreach (var student in students)
            {
                _students.Add(student);
            }
        }

        // Show the add student form
        private void ShowAddStudentForm_Clicked(object sender, EventArgs e)
        {
            FormBorder.IsVisible = true;
        }

        // Save the student (either new or updated)
        private async void SaveStudent_Clicked(object sender, EventArgs e)
        {
            var student = new Student
            {
                StudentName = StudentNameEntry.Text,
                MatricNum = MatricNumEntry.Text,
                Email = EmailEntry.Text,
                PhoneNum = PhoneNumEntry.Text,
                Password = PasswordEntry.Text,
                Block = (Block)Enum.Parse(typeof(Block), BlockPicker.SelectedItem.ToString()), // Convert selected item to Block enum
                College = (College)Enum.Parse(typeof(College), CollegePicker.SelectedItem.ToString()), // Convert selected item to College enum
                RoomNo = RoomNoEntry.Text
            };

            if (!string.IsNullOrWhiteSpace(student.MatricNum))
            {
                
                var existingStudent = await _databaseService.GetStudentByMatricNumAsync(student.MatricNum);
                if (existingStudent != null)
                {
                    student.StudentId = existingStudent.StudentId;  // Ensure the existing student ID is used
                    await _databaseService.UpdateStudentAsync(student);
                }
                else
                {
                    
                    await _databaseService.InsertStudentAsync(student);
                }

                LoadStudentsAsync(); // Refresh the student list
                FormBorder.IsVisible = false; // Hide the form
            }
        }

        // Cancel and hide the form
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            FormBorder.IsVisible = false;
        }

        // Edit a student (populate the form with existing student data)
        private void EditStudent_Clicked(object sender, EventArgs e)
        {
            var student = (Student)((Button)sender).CommandParameter;
            StudentNameEntry.Text = student.StudentName;
            MatricNumEntry.Text = student.MatricNum;
            EmailEntry.Text = student.Email;
            PhoneNumEntry.Text = student.PhoneNum;
            PasswordEntry.Text = student.Password;
            BlockPicker.SelectedItem = student.Block;
            CollegePicker.SelectedItem = student.College;
            RoomNoEntry.Text = student.RoomNo;

            FormBorder.IsVisible = true; // Show the form to edit the student
        }

        // Delete a student
        private async void DeleteStudent_Clicked(object sender, EventArgs e)
        {
            var student = (Student)((Button)sender).CommandParameter;
            if (student != null)
            {
                await _databaseService.DeleteStudentAsync(student); // Delete the student from the database
                LoadStudentsAsync(); // Refresh the student list
            }
        }
    }
}
