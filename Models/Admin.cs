using System;
using SQLite;
using System.ComponentModel;

namespace KKDK_Parcel_Delivery.Models
{
    public class Admin : INotifyPropertyChanged
    {
        private int _adminId;
        private string _adminName;
        private string _matricNum;
        private string _password;
        private string _phoneNum;

        [PrimaryKey, AutoIncrement]
        public int AdminId
        {
            get => _adminId;
            set
            {
                if (_adminId != value)
                {
                    _adminId = value;
                    OnPropertyChanged(nameof(AdminId));
                }
            }
        }

        public string AdminName
        {
            get => _adminName;
            set
            {
                if (_adminName != value)
                {
                    _adminName = value;
                    OnPropertyChanged(nameof(AdminName));
                }
            }
        }

        public string MatricNum
        {
            get => _matricNum;
            set
            {
                if (_matricNum != value)
                {
                    _matricNum = value;
                    OnPropertyChanged(nameof(MatricNum));
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public string PhoneNum
        {
            get => _phoneNum;
            set
            {
                if (_phoneNum != value)
                {
                    _phoneNum = value;
                    OnPropertyChanged(nameof(PhoneNum));
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
