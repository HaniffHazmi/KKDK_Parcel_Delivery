using KKDK_Parcel_Delivery.Models;
using KKDK_Parcel_Delivery.Services;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KKDK_Parcel_Delivery.Pages.AdminPages;

public partial class AdminParcelListPage : ContentPage
{
	public ObservableCollection<Parcel> Parcels { get; set; }
	
    public DatabaseService _database;
    public ICommand ParcelTappedCommand { get; set; }

    public AdminParcelListPage()
	{
		InitializeComponent();
        BindingContext = this;
        _database = new DatabaseService();
        LoadParcels();
        ParcelTappedCommand = new Command<Parcel>(async(parcel) => await OnParcelTapped(parcel));
        
    }

    private async Task LoadParcels()
    {
        
        var parcels = await _database.GetAllParcelsAsync();
        Parcels = new ObservableCollection<Parcel>(parcels);
        OnPropertyChanged(nameof(Parcels));
    }

    private async void OnParcelSelected(object sender, SelectionChangedEventArgs e)
    {
        var SelectedParcel = e.CurrentSelection.FirstOrDefault() as Parcel;
        if (SelectedParcel != null)
        {
            await Navigation.PushAsync(new ParcelDetailsPage(SelectedParcel));
        }
    }

    private async Task OnParcelTapped(Parcel parcel)
    {
        await Navigation.PushAsync(new ParcelDetailsPage(parcel));

        await LoadParcels();
    }
}