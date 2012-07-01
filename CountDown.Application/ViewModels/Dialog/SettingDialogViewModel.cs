using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using BigEgg.Framework.Foundation.Validations;
using CountDown.Application.Properties;
using CountDown.Application.Views.Dialog;
using System.ComponentModel;
using BigEgg.Framework.Applications.Services;
using CountDown.Application.Services;
using System.Globalization;

namespace CountDown.Application.ViewModels.Dialog
{
    [Export]
    public class SettingDialogViewModel : DialogViewModel<ISettingDialogView>
    {
        #region Members
        private readonly DelegateCommand submitCommand;
        private readonly DelegateCommand cancelCommand;
        private readonly IMessageService messageService;
        private readonly IShellService shellService;

        private bool hasAlertSound;
        #endregion

        [ImportingConstructor]
        public SettingDialogViewModel(ISettingDialogView view, IMessageService messageService, IShellService shellService)
            : base(view)
        {
            this.submitCommand = new DelegateCommand(SaveSettingCommand);
            this.cancelCommand = new DelegateCommand(() => Close(false));
            this.messageService = messageService;
            this.shellService = shellService;

            this.BeforeAlertMinutes = Settings.Default.DefautBeforeAlertMinutes;
            this.ExpiredMinutes = Settings.Default.DefaultExpiredMinutes;
            this.HasAlertSound = Settings.Default.HasAlertSound;
            this.SoundPath = Settings.Default.SoundPath;
            this.ResetCountDownData = Settings.Default.ResetCountDownData;
        }

        #region Properties
        public ICommand SubmitCommand { get { return this.submitCommand; } }

        public ICommand CancelCommand { get { return this.cancelCommand; } }

        [Required(ErrorMessageResourceName = "BeforeAlertMinutesMandatory", ErrorMessageResourceType = typeof(Resources))]
        [Range(1, 65535, ErrorMessageResourceName = "BeforeAlertMinutesRange", ErrorMessageResourceType = typeof(Resources))]
        public int BeforeAlertMinutes { get; set; }

        [Required(ErrorMessageResourceName = "ExpiredMinutesMandatory", ErrorMessageResourceType = typeof(Resources))]
        [Range(1, 65535, ErrorMessageResourceName = "ExpiredMinutesRange", ErrorMessageResourceType = typeof(Resources))]
        public int ExpiredMinutes { get; set; }

        public bool HasAlertSound 
        {
            get { return this.hasAlertSound; }
            set
            {
                this.hasAlertSound = value;
                if (!this.hasAlertSound)
                    SoundPath = string.Empty;
            }
        }

        [Path(ErrorMessageResourceName = "SoundPathError", ErrorMessageResourceType = typeof(Resources))]
        [RequiredIf("HasAlertSound", true, ErrorMessageResourceName = "SoundPathMandatory", ErrorMessageResourceType = typeof(Resources))]
        public string SoundPath { get; set; }

        public bool ResetCountDownData { get; set; }
        #endregion

        private void SaveSettingCommand()
        {
            if (!string.IsNullOrWhiteSpace(this.dataErrorInfoSupport.Error))
            {
                messageService.ShowError(shellService.ShellView, 
                    string.Format(CultureInfo.CurrentCulture, Resources.SettingError));
                return;
            }

            Settings.Default.DefautBeforeAlertMinutes = this.BeforeAlertMinutes;
            Settings.Default.DefaultExpiredMinutes = this.ExpiredMinutes;
            Settings.Default.HasAlertSound = this.HasAlertSound;
            Settings.Default.SoundPath = this.SoundPath;
            Settings.Default.ResetCountDownData = this.ResetCountDownData;
            Settings.Default.Save();

            Close(true);
        }
    }
}
