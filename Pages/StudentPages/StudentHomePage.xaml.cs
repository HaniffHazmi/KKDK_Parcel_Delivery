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

    }
}

