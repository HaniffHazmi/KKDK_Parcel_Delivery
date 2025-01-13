using System;
using SQLite;
using System.ComponentModel;

namespace KKDK_Parcel_Delivery.Models
{
    public enum Block
    {
        A,
        B,
        C,
        D
    }

    public enum College
    {
        TDI,
        TF
    }

    public class Student : INotifyPropertyChanged
    {
        private int _studentId;
        private string _studentName;
        private string _matricNum;
        private string _email;
        private string _phoneNum;
        private string _password;
        private Block _block;
        private College _college;
        private string _roomNo;

        [PrimaryKey, AutoIncrement]
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

        public string StudentName
        {
            get => _studentName;
            set
            {
                if (_studentName != value)
                {
                    _studentName = value;
                    OnPropertyChanged(nameof(StudentName));
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

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
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

        public Block Block
        {
            get => _block;
            set
            {
                if (_block != value)
                {
                    _block = value;
                    OnPropertyChanged(nameof(Block));
                }
            }
        }

        public College College
        {
            get => _college;
            set
            {
                if (_college != value)
                {
                    _college = value;
                    OnPropertyChanged(nameof(College));
                }
            }
        }

        public string RoomNo
        {
            get => _roomNo;
            set
            {
                if (_roomNo != value)
                {
                    _roomNo = value;
                    OnPropertyChanged(nameof(RoomNo));
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
