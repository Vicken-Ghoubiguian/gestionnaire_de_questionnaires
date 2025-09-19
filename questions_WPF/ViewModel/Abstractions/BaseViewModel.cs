using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace questions_WPF.ViewModel.Abstractions;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (this.PropertyChanged is not null && !string.IsNullOrEmpty(propertyName))
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}