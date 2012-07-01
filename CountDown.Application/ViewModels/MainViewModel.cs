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
        private readonly ISettingDialogView settingDialog;

        private readonly IMessageService messageService;
        private readonly IShellService shellService;
        private readonly IDataService dataService;
        private readonly DelegateCommand englishCommand;
        private readonly DelegateCommand chineseCommand;
        private readonly DelegateCommand aboutCommand;
        private ICommand settingCommand;
        private ICommand exitCommand;
        private CultureInfo newLanguage;

        private bool hasAlertSound;
        private bool resetCountDownData;

        [ImportingConstructor]
        public MainViewModel(IMainView view, IAboutDialogView aboutDialog, ISettingDialogView settingDialog,
            IMessageService messageService, IShellService shellService, IDataService dataService)
            : base(view)
        {
            this.aboutDialog = aboutDialog;
            this.settingDialog = settingDialog;

            this.messageService = messageService;
            this.shellService = shellService;
            this.dataService = dataService;
            this.englishCommand = new DelegateCommand(() => SelectLanguage(new CultureInfo("en-US")));
            this.chineseCommand = new DelegateCommand(() => SelectLanguage(new CultureInfo("zh-CN")));
            this.aboutCommand = new DelegateCommand(ShowAboutMessage);

            this.hasAlertSound = Settings.Default.HasAlertSound;
            this.resetCountDownData = Settings.Default.ResetCountDownData;
        }

        #region Properties
        public IDataService DataService { get { return this.dataService; } }

        public CultureInfo NewLanguage { get { return this.newLanguage; } }

        public bool HasAlertSound
        {
            get { return this.hasAlertSound; }
            set
            {
                if (this.hasAlertSound != value)
                {
                    this.hasAlertSound = value;
                    Settings.Default.HasAlertSound = this.hasAlertSound;
                    Settings.Default.Save();
                }
            }
        }

        public bool ResetCountDownData
        {
            get { return this.resetCountDownData; }
            set
            {
                if (this.resetCountDownData != value)
                {
                    this.resetCountDownData = value;
                    Settings.Default.ResetCountDownData = this.resetCountDownData;
                    Settings.Default.Save();
                }
            }
        }

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

        public ICommand SettingCommand
        {
            get { return this.settingCommand; }
            set
            {
                if (this.settingCommand != value)
                {
                    this.settingCommand = value;
                    RaisePropertyChanged("SettingCommand");
                }
            }
        }

        public ICommand EnglishCommand { get { return this.englishCommand; } }

        public ICommand ChineseCommand { get { return this.chineseCommand; } }

        public ICommand AboutCommand { get { return this.aboutCommand; } }
        #endregion

        private void SelectLanguage(CultureInfo uiCulture)
        {
            if (!uiCulture.Equals(CultureInfo.CurrentUICulture))
            {
                messageService.ShowMessage(shellService.ShellView, Resources.RestartApplication + "\n\n" +
                    Resources.ResourceManager.GetString("RestartApplication", uiCulture));
            }
            this.newLanguage = uiCulture;
        }

        private void ShowAboutMessage()
        {
            aboutDialog.ShowDialog(shellService.ShellView);
        }
    }
}
