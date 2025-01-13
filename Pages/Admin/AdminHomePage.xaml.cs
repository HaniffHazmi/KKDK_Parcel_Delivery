using System;
using KKDK_Parcel_Delivery.Models;
using Microsoft.Maui.Controls;

namespace KKDK_Parcel_Delivery.Pages.Admin;

public partial class AdminHomePage : ContentPage
{
	public AdminHomePage()
	{
		InitializeComponent();
	}

    /*private async void OnViewAllParcelsClicked(object sender, EventArgs e)
    {
        // Navigate to the page where admin can view all parcels
        await Navigation.PushAsync(new ViewAllParcelsPage());
    }

    private async void OnManageAdminsClicked(object sender, EventArgs e)
    {
        // Navigate to the page where admin can manage other admins
        await Navigation.PushAsync(new ManageAdminsPage());
    }*/

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Navigate back to the login page
        await Navigation.PopToRootAsync();
    }
}