using System;
using KKDK_Parcel_Delivery.Models;
using Microsoft.Maui.Controls;

namespace KKDK_Parcel_Delivery.Pages.AdminPages;

public partial class AdminHomePage : ContentPage
{
	public AdminHomePage()
	{
		InitializeComponent();
	}

    

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Navigate back to the login page
        await Navigation.PopToRootAsync();
    }
}