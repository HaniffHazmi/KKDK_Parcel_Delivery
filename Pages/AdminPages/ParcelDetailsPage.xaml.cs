using KKDK_Parcel_Delivery.Models;
using KKDK_Parcel_Delivery.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace KKDK_Parcel_Delivery.Pages.AdminPages;

public partial class ParcelDetailsPage : ContentPage
{
    public Parcel Parcel { get; set; }
    public ObservableCollection<string> ParcelStatusList { get; set; }
    public ICommand SaveCommand { get; set; }
    public ICommand EditCommand { get; set; }

    public DatabaseService _database;

    private bool _isEditing;
    public bool IsEditing
    {
        get => _isEditing;
        set
        {
            _isEditing = value;
            OnPropertyChanged(nameof(IsEditing));
        }
    }


    public ParcelDetailsPage(Parcel parcel)
    {
        InitializeComponent();
        Parcel = parcel;

        // Initialize status list
        ParcelStatusList = new ObservableCollection<string>
        {
            "Pending", "Found", "Delivered", "NotFound"
        };

        // Initialize commands
        EditCommand = new Command(() => StartEditing());
        SaveCommand = new Command(async () => await SaveParcelStatus());

        // Initially, we are not in edit mode
        IsEditing = false;

        BindingContext = this;

        // Database service instantiation
        _database = new DatabaseService();
    }

    // Switch to edit mode
    private void StartEditing()
    {
        IsEditing = true;
        
    }

    // Save the updated parcel status
    private async Task SaveParcelStatus()
    {
        await _database.UpdateParcelStatusAsync(Parcel);
        
        await DisplayAlert("Success", "Parcel status updated", "OK");
        IsEditing = false; // Exit edit mode after saving
        OnPropertyChanged(nameof(IsEditing));
        await Navigation.PopAsync();
        await Navigation.PopAsync();
    }
}