using System;
using SQLite;
using System.ComponentModel;

namespace KKDK_Parcel_Delivery.Models
{
    public enum Courier
    {
        JNT,
        SPX,
        Flash,
        Ninjavan
    }

    public enum ParcelStatus
    {
        Pending,
        Found,
        NotFound,
        Delivered
    }

    public class Parcel : INotifyPropertyChanged
    {
        private int _parcelId;
        private string _trackingNumber;
        private Courier _courier;
        private DateTime _dateArrived;
        private ParcelStatus _parcelStatus;
        private string _receiverName;
        private int _studentId;  

        [PrimaryKey, AutoIncrement]
        public int ParcelId
        {
            get => _parcelId;
            set
            {
                if (_parcelId != value)
                {
                    _parcelId = value;
                    OnPropertyChanged(nameof(ParcelId));
                }
            }
        }

        public string TrackingNumber
        {
            get => _trackingNumber;
            set
            {
                if (_trackingNumber != value)
                {
                    _trackingNumber = value;
                    OnPropertyChanged(nameof(TrackingNumber));
                }
            }
        }

        public Courier Courier
        {
            get => _courier;
            set
            {
                if (_courier != value)
                {
                    _courier = value;
                    OnPropertyChanged(nameof(Courier));
                }
            }
        }

        public DateTime DateArrived
        {
            get => _dateArrived;
            set
            {
                if (_dateArrived != value)
                {
                    _dateArrived = value;
                    OnPropertyChanged(nameof(DateArrived));
                }
            }
        }

        public ParcelStatus ParcelStatus
        {
            get => _parcelStatus;
            set
            {
                if (_parcelStatus != value)
                {
                    _parcelStatus = value;
                    OnPropertyChanged(nameof(ParcelStatus));
                }
            }
        }

        public string ReceiverName
        {
            get => _receiverName;
            set
            {
                if (_receiverName != value)
                {
                    _receiverName = value;
                    OnPropertyChanged(nameof(ReceiverName));
                }
            }
        }

        public int StudentId
        {
            get => _studentId;
            set
            {
                if (_studentId != value)
                {
                    _studentId = value;
                    OnPropertyChanged(nameof(StudentId));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
