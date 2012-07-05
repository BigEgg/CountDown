using System.Windows.Input;
using BigEgg.Framework.Applications;
using CountDown.Applications.Properties;
using CountDown.Applications.Views.Dialog;

namespace CountDown.Applications.ViewModels.Dialog
{
    public class AboutDialogViewModel : DialogViewModel<IAboutDialogView>
    {
        private readonly DelegateCommand okCommand;


        public AboutDialogViewModel(IAboutDialogView view)
            : base(view)
        {
            this.okCommand = new DelegateCommand(() => Close(true));
        }


        public string ProductName { get { return Resources.ApplicationName; } }

        public string Version { get { return ApplicationInfo.Version; } }


        public ICommand OKCommand { get { return this.okCommand; } }
    }
}
