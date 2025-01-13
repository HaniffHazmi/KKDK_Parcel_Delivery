using KKDK_Parcel_Delivery.Models;
using KKDK_Parcel_Delivery.Services;
using Microsoft.Maui.Controls;


namespace KKDK_Parcel_Delivery.Pages.StudentPages;

public partial class ParcelDetailPage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private int _parcelId;

    public ParcelDetailPage(int parcelId)
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
        _parcelId = parcelId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Get the parcel details from the database
        var parcel = await _databaseService.GetParcelByIdAsync(_parcelId);

        if (parcel != null)
        {
            // Display the parcel details
            ParcelIdLabel.Text = parcel.ParcelId.ToString();
            TrackingNumberLabel.Text = parcel.TrackingNumber;
            CourierLabel.Text = parcel.Courier.ToString();
            DateArrivedLabel.Text = parcel.DateArrived.ToString("yyyy-MM-dd");
            ParcelStatusLabel.Text = parcel.ParcelStatus.ToString();
            ReceiverNameLabel.Text = parcel.ReceiverName;
        }
        else
        {
            await DisplayAlert("Error", "Parcel not found", "OK");
        }
    }

    // Handle the back button click event
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); // Navigate back to the previous page
    }
}