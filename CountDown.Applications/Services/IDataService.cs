using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CountDown.Applications.Domain;
using CountDown.Applications.Models;

namespace CountDown.Applications.Services
{
    public interface IDataService : INotifyPropertyChanged
    {
        ObservableCollection<ICountDownItem> CountDownItems { get; }

        ObservableCollection<ICountDownItem> AlertItems { get; }

        ObservableCollection<ICountDownItem> SelectItems { get; }

        ObservableCollection<string> Branches { get; }

        NewCountDownModel NewCountDownModel { get; }

        ICommand NewCountDownItem { get; }

        ICommand DeleteCountDownItem { get; }

        void CleanExpiredItems();

        void CheckAlertItems();
    }
}
