using System.ComponentModel;

namespace CountDown.Applications.Services
{
    public interface IShellService : INotifyPropertyChanged
    {
        object ShellView { get; }

        object ItemListView { get; set; }

        object NewItemsView { get; set; }
    }
}
