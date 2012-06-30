using System.ComponentModel;

namespace CountDown.Application.Services
{
    public interface IShellService : INotifyPropertyChanged
    {
        object ShellView { get; }
    }
}
