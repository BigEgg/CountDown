using System.Collections.ObjectModel;
using System.ComponentModel;
using CountDown.Applications.Domain;

namespace CountDown.Applications.Services
{
    public interface IDataService : INotifyPropertyChanged
    {
        MultiThreadingObservableCollection<IAlertItem> Items { get; }

        MultiThreadingObservableCollection<IAlertItem> AlertedItems { get; }

        IAlertItem SelectedItem { get; set; }

        ObservableCollection<IAlertItem> SelectedItems { get; }

        ObservableCollection<string> Branches { get; }
    }
}
