using System;
using KKDK_Parcel_Delivery.Models;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;


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

    private async void OnParcelListClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdminParcelListPage());
    }

    private async void OnAdminListClicked(object sender, EventArgs e)
    {
        // Navigate to the AdminListPage
        await Navigation.PushAsync(new AdminListPage());
    }

    private async void OnStudentListClicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new StudentListPage());
    }
}