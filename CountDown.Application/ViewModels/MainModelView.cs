using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using BigEgg.Framework.Applications.Services;
using CountDown.Application.Properties;
using CountDown.Application.Services;
using CountDown.Application.Views;
using CountDown.Application.Views.Dialog;

namespace CountDown.Application.ViewModels
{
    [Export]
    public class MainViewModel : ViewModel<IMainView>
    {
        private readonly IAboutDialogView aboutDialog;

        private readonly IMessageService messageService;
        private readonly IShellService shellService;
        private readonly IDataService dataService;
        private readonly DelegateCommand englishCommand;
        private readonly DelegateCommand chineseCommand;
        private readonly DelegateCommand aboutCommand;
        private ICommand exitCommand;
        private CultureInfo newLanguage;


        [ImportingConstructor]
        public MainViewModel(IMainView view, IAboutDialogView aboutDialog, IMessageService messageService, IShellService shellService, IDataService dataService)
            : base(view)
        {
            this.aboutDialog = aboutDialog;

            this.messageService = messageService;
            this.shellService = shellService;
            this.dataService = dataService;
            this.englishCommand = new DelegateCommand(() => SelectLanguage(new CultureInfo("en-US")));
            this.chineseCommand = new DelegateCommand(() => SelectLanguage(new CultureInfo("zh-CN")));
            this.aboutCommand = new DelegateCommand(ShowAboutMessage);
        }


        public IDataService DataService { get { return this.dataService; } }

        public CultureInfo NewLanguage { get { return this.newLanguage; } }


        public ICommand ExitCommand
        {
            get { return this.exitCommand; }
            set
            {
                if (this.exitCommand != value)
                {
                    this.exitCommand = value;
                    RaisePropertyChanged("ExitCommand");
                }
            }
        }

        public ICommand EnglishCommand { get { return this.englishCommand; } }

        public ICommand ChineseCommand { get { return this.chineseCommand; } }

        public ICommand AboutCommand { get { return this.aboutCommand; } }


        private void SelectLanguage(CultureInfo uiCulture)
        {
            if (!uiCulture.Equals(CultureInfo.CurrentUICulture))
            {
                messageService.ShowMessage(shellService.ShellView, Resources.RestartApplication + "\n\n" +
                    Resources.ResourceManager.GetString("RestartApplication", uiCulture));
            }
            newLanguage = uiCulture;
        }

        private void ShowAboutMessage()
        {
            aboutDialog.ShowDialog(shellService.ShellView);
        }
    }
}
