using System.ComponentModel.Composition;
using BigEgg.Framework.Applications;

namespace CountDown.Application.Services
{
    [Export(typeof(IShellService)), Export]
    internal class ShellService : DataModel, IShellService
    {
        private object shellView;

        [ImportingConstructor]
        public ShellService()
        {
        }

        public object ShellView
        {
            get { return shellView; }
            set
            {
                if (shellView != value)
                {
                    shellView = value;
                    RaisePropertyChanged("ShellView");
                }
            }
        }
    }
}
