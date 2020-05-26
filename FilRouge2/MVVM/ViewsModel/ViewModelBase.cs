using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace FilRouge2
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisepropertyChanged([CallerMemberName]string propertyName = "")
        { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
