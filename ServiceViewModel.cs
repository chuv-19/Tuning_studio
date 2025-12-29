using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Prototype
{
    public class ServiceViewModel : INotifyPropertyChanged
    {
        private string _name;
        private bool _isSelected;
        private ObservableCollection<ServiceViewModel> _subServices = new ObservableCollection<ServiceViewModel>();

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ServiceViewModel> SubServices
        {
            get => _subServices;
            set { _subServices = value; OnPropertyChanged(); }
        }

        public bool HasSubServices => SubServices.Count > 0;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
