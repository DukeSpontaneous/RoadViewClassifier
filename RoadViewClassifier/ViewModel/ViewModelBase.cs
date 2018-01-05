using System.Windows;
using System.ComponentModel;

namespace RoadViewClassifier.ViewModel
{
    class ViewModelBase : INotifyPropertyChanged
    {
        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
