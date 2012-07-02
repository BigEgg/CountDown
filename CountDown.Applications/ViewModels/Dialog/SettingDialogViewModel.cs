using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using BigEgg.Framework.Foundation.Validations;
using CountDown.Applications.Properties;
using CountDown.Applications.Services;
using CountDown.Applications.Views.Dialog;

namespace CountDown.Applications.ViewModels.Dialog
{
    [Export]
    public class SettingDialogViewModel : DialogViewModel<ISettingDialogView>
    {
        #region Members
        private readonly DelegateCommand submitCommand;
        private readonly DelegateCommand cancelCommand;

        private readonly ObservableCollection<string> branches;
        private readonly ObservableCollection<string> selectedBranches;
        private readonly DelegateCommand addNewCommand;
        private readonly DelegateCommand removeCommand;
        private string newBranch = string.Empty;
        private bool hasAlertSound = false;
        #endregion

        [ImportingConstructor]
        public SettingDialogViewModel(ISettingDialogView view, IDataService dataservice)
            : base(view)
        {
            this.submitCommand = new DelegateCommand(() => Close(true), () => string.IsNullOrEmpty(this.dataErrorInfoSupport.Error));
            this.cancelCommand = new DelegateCommand(() => Close(false));

            this.branches = dataservice.Branches;
            this.selectedBranches = new ObservableCollection<string>();

            this.addNewCommand = new DelegateCommand(AddNewBranch, CanAddNewBranch);
            this.removeCommand = new DelegateCommand(RemoveBranch, CanRemoveBranch);
        }

        #region Properties
        public ICommand SubmitCommand { get { return this.submitCommand; } }

        public ICommand CancelCommand { get { return this.cancelCommand; } }

        public ICommand AddNewCommand { get { return this.addNewCommand; } }

        public ICommand RemoveCommand { get { return this.removeCommand; } }

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

        public string NewBranch
        {
            get { return this.newBranch; }
            set
            {
                if (this.newBranch != value)
                {
                    this.newBranch = value;
                    RaisePropertyChanged("NewBranch");
                }
            }
        }

        public ObservableCollection<string> Branches { get { return this.branches; } }

        public ObservableCollection<string> SelectedBranches 
        { 
            get 
            {
                if ((!this.selectedBranches.Any()) && (this.branches.Any()))
                    this.selectedBranches.Add(this.branches.First());
                return this.selectedBranches; 
            } 
        }
        #endregion

        private bool CanAddNewBranch() { return (!string.IsNullOrEmpty(this.NewBranch)); }

        private void AddNewBranch()
        {
            if (!this.branches.Contains(this.NewBranch))
            {
                this.branches.Add(this.NewBranch);
            }
            this.selectedBranches.Clear();
            this.selectedBranches.Add(this.NewBranch);
            this.NewBranch = string.Empty;
        }

        private bool CanRemoveBranch() { return this.SelectedBranches.Any(); }

        private void RemoveBranch()
        {
            foreach (string item in this.selectedBranches)
            {
                this.branches.Remove(item);
            }
            this.selectedBranches.Clear();
        }
    }
}
