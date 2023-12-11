// TableItem.cs

using System.ComponentModel;

namespace HelloWorld.ViewModels
{
    public class TableItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _data;
        private bool _isEven;

        public string Data
        {
            get => _data;
            set
            {
                if (_data != value)
                {
                    _data = value;
                    OnPropertyChanged(nameof(Data));
                }
            }
        }

        public bool IsEven
        {
            get => _isEven;
            set
            {
                if (_isEven != value)
                {
                    _isEven = value;
                    OnPropertyChanged(nameof(IsEven));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
