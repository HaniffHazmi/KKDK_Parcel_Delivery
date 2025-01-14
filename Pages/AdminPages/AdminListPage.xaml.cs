using KKDK_Parcel_Delivery.Models;
using KKDK_Parcel_Delivery.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace KKDK_Parcel_Delivery.Pages.AdminPages
{
    public partial class AdminListPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<Admin> _adminList;
        private Admin _selectedAdmin;

        public AdminListPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            LoadAdmins();
        }

        private async void LoadAdmins()
        {
            var admins = await _databaseService.GetAdminsAsync();
            _adminList = new ObservableCollection<Admin>(admins);
            AdminCollectionView.ItemsSource = _adminList;
        }

        // Show the form to add a new admin
        private void ShowAddAdminForm_Clicked(object sender, EventArgs e)
        {
            _selectedAdmin = null;  // Reset to add a new admin
            AdminNameEntry.Text = string.Empty;
            MatricNumEntry.Text = string.Empty;
            PhoneNumEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;

            SaveAdminButton.Text = "Add Admin";
            FormBorder.IsVisible = true;
            AddNewAdminButton.IsVisible = false;
        }

        // Show the form to edit an existing admin
        private void EditAdmin_Clicked(object sender, EventArgs e)
        {
            var admin = (sender as Button).CommandParameter as Admin;
            if (admin != null)
            {
                _selectedAdmin = admin;
                AdminNameEntry.Text = _selectedAdmin.AdminName;
                MatricNumEntry.Text = _selectedAdmin.MatricNum;
                PhoneNumEntry.Text = _selectedAdmin.PhoneNum;
                PasswordEntry.Text = _selectedAdmin.Password;

                SaveAdminButton.Text = "Save Changes";
                FormBorder.IsVisible = true;
                AddNewAdminButton.IsVisible = false;
            }
        }

        // Save the new or edited admin
        private async void SaveAdmin_Clicked(object sender, EventArgs e)
        {
            if (_selectedAdmin == null)
            {
                // Adding a new admin
                var newAdmin = new Admin
                {
                    AdminName = AdminNameEntry.Text,
                    MatricNum = MatricNumEntry.Text,
                    PhoneNum = PhoneNumEntry.Text,
                    Password = PasswordEntry.Text
                };
                await _databaseService.InsertAdminAsync(newAdmin);
                _adminList.Add(newAdmin);
            }
            else
            {
                // Editing an existing admin
                _selectedAdmin.AdminName = AdminNameEntry.Text;
                _selectedAdmin.MatricNum = MatricNumEntry.Text;
                _selectedAdmin.PhoneNum = PhoneNumEntry.Text;
                _selectedAdmin.Password = PasswordEntry.Text;
                await _databaseService.UpdateAdminAsync(_selectedAdmin);

                // Refresh the list to reflect changes
                int index = _adminList.IndexOf(_selectedAdmin);
                _adminList[index] = _selectedAdmin;
            }

            FormBorder.IsVisible = false;
            AddNewAdminButton.IsVisible = true;
        }

        // Delete an admin
        private async void DeleteAdmin_Clicked(object sender, EventArgs e)
        {
            var admin = (sender as Button).CommandParameter as Admin;
            if (admin != null)
            {
                await _databaseService.DeleteAdminAsync(admin);
                _adminList.Remove(admin);
            }
        }

        // Cancel the add/edit form
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            FormBorder.IsVisible = false;
            AddNewAdminButton.IsVisible = true;
        }
    }

}
