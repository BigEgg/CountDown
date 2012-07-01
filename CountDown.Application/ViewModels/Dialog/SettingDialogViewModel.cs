using System.ComponentModel.Composition;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using CountDown.Application.Views.Dialog;
using CountDown.Application.Properties;
using System.ComponentModel.DataAnnotations;
using BigEgg.Framework.Foundation.Validations;

namespace CountDown.Application.ViewModels.Dialog
{
    [Export]
    [RequiredIf("SoundPath", "HasAlertSound", true, ErrorMessageResourceName = "SoundPathMandatory", ErrorMessageResourceType = typeof(Resources))]
    public class SettingDialogViewModel : DialogViewModel<ISettingDialogView>
    {
        #region Members
        private readonly DelegateCommand submitCommand;
        private readonly DelegateCommand cancelCommand;

        private bool hasAlertSound;
        #endregion

        [ImportingConstructor]
        public SettingDialogViewModel(ISettingDialogView view)
            : base(view)
        {
            this.submitCommand = new DelegateCommand(SaveSettingCommand);
            this.cancelCommand = new DelegateCommand(() => Close(false));

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
        public string SoundPath { get; set; }

        public bool ResetCountDownData { get; set; }
        #endregion

        private void SaveSettingCommand()
        {
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
