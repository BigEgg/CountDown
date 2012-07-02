using System.ComponentModel;

namespace CountDown.Applications.Services
{
    public interface IShellService : INotifyPropertyChanged
    {
        object ShellView { get; }
    }
}
