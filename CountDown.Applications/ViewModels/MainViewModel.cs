using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using BigEgg.Framework.Applications.Services;
using CountDown.Applications.Properties;
using CountDown.Applications.Services;
using CountDown.Applications.Views;
using CountDown.Applications.Views.Dialog;

namespace CountDown.Applications.ViewModels
{
    [Export]
    public class MainViewModel : ViewModel<IMainView>
    {
        private readonly IMessageService messageService;
        private readonly IShellService shellService;
        private readonly IDataService dataService;
        private readonly DelegateCommand englishCommand;
        private readonly DelegateCommand chineseCommand;
        private ICommand aboutCommand;
        private ICommand settingCommand;
        private ICommand exitCommand;
        private CultureInfo newLanguage;

        private bool hasAlertSound;
        private bool resetCountDownData;

        [ImportingConstructor]
        public MainViewModel(IMainView view, IMessageService messageService, 
            IShellService shellService, IDataService dataService)
            : base(view)
        {

            this.messageService = messageService;
            this.shellService = shellService;
            this.dataService = dataService;
            this.englishCommand = new DelegateCommand(() => SelectLanguage(new CultureInfo("en-US")));
            this.chineseCommand = new DelegateCommand(() => SelectLanguage(new CultureInfo("zh-CN")));

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
                    RaisePropertyChanged("HasAlertSound");
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
                    RaisePropertyChanged("ResetCountDownData");
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

        public ICommand AboutCommand
        {
            get { return this.aboutCommand; }
            set
            {
                if (this.aboutCommand != value)
                {
                    this.aboutCommand = value;
                    RaisePropertyChanged("AboutCommand");
                }
            }
        }

        public ICommand EnglishCommand { get { return this.englishCommand; } }

        public ICommand ChineseCommand { get { return this.chineseCommand; } }
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
    }
}
