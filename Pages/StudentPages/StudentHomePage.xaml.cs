using System;
using KKDK_Parcel_Delivery.Models;
using Microsoft.Maui.Controls;

namespace KKDK_Parcel_Delivery.Pages.StudentPages 
{
    public partial class StudentHomePage : ContentPage
    {
        public StudentHomePage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void OnParcelListClicked(object sender, EventArgs e)
        {
            // Navigate to the ParcelListPage for the student
            await Navigation.PushAsync(new ParcelListPage());
        }


    }
}

