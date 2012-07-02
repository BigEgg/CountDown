using System.ComponentModel.Composition;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using CountDown.Applications.Views.Dialog;

namespace CountDown.Applications.ViewModels.Dialog
{
    [Export]
    public class AboutDialogViewModel : DialogViewModel<IAboutDialogView>
    {
        private readonly DelegateCommand okCommand;


        [ImportingConstructor]
        public AboutDialogViewModel(IAboutDialogView view)
            : base(view)
        {
            this.okCommand = new DelegateCommand(() => Close(true));
        }


        public string ProductName { get { return ApplicationInfo.ProductName; } }

        public string Version { get { return ApplicationInfo.Version; } }


        public ICommand OKCommand { get { return this.okCommand; } }
    }
}
