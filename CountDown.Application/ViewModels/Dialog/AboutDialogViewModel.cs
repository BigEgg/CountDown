using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using CountDown.Application.Properties;
using CountDown.Application.Views.Dialog;

namespace CountDown.Application.ViewModels.Dialog
{
    [Export]
    public class AboutDialogViewModel : DialogViewModel<IAboutDialog>
    {
        private readonly DelegateCommand okCommand;


        [ImportingConstructor]
        public AboutDialogViewModel(IAboutDialog view)
            : base(view)
        {
            this.okCommand = new DelegateCommand(() => Close(true));
        }


        public string ProductName { get { return ApplicationInfo.ProductName; } }

        public string Version { get { return ApplicationInfo.Version; } }

        public string Message { get { return string.Format(CultureInfo.CurrentCulture, Resources.AboutMessage); } }


        public ICommand OKCommand { get { return this.okCommand; } }
    }
}
