using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WpfApplication1
{
    public class Employee : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _cityName;
        private DateTime _releaseDate;
        private IEnumerable<MyData> _myDatas;

        public Employee()
        {
        }

        public Employee(string firstName, string lastName, string cityName, DateTime releaseDate, IEnumerable<MyData> myDatas)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.CityName = cityName;
            this.ReleaseDate = releaseDate;
            this.MyDatas = myDatas;
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }

        public string CityName
        {
            get
            {
                return _cityName;
            }
            set 
            {
                _cityName = value;
                NotifyPropertyChanged("CityName");
            }
        }

        public DateTime ReleaseDate
        {
            get
            {
                return _releaseDate;
            }
            set
            {
                _releaseDate = value;
                NotifyPropertyChanged("ReleaseDate");
            }
        }

        public IEnumerable<MyData> MyDatas
        {
            get
            {
                return _myDatas ?? (_myDatas = new List<MyData>());
            }
            set
            {
                _myDatas = value;
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}