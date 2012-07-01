using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using CountDown.Application.Domain;
using CountDown.Application.Models;

namespace CountDown.Application.Services
{
    public interface IDataService : INotifyPropertyChanged
    {
        ObservableCollection<ICountDownItem> CountDownItems { get; }

        ObservableCollection<ICountDownItem> AlartItems { get; }

        ObservableCollection<ICountDownItem> SelectItems { get; }

        ObservableCollection<string> Branches { get; }

        NewCountDownModel NewCountDownModel { get; }

        ICommand NewCountDownItem { get; }

        ICommand DeleteCountDownItem { get; }

        void CleanExpiredItems();

        void CheckAlartItems();
    }
}
