using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using BigEgg.Framework.Applications.Services;
using BigEgg.Framework.Foundation.Validations;
using CountDown.Applications.Properties;
using CountDown.Applications.Services;
using CountDown.Applications.Views.Dialogs;

namespace CountDown.Applications.ViewModels.Dialogs
{
    public class SettingDialogViewModel : DialogViewModel<ISettingDialogView>
    {
        #region Members
        private readonly ObservableCollection<string> branches;
        private readonly DelegateCommand submitCommand;
        private readonly DelegateCommand cancelCommand;
        private readonly DelegateCommand browseSoundFile;
        private readonly DelegateCommand addNewBranchCommand;
        private readonly DelegateCommand removeBranchCommand;

        private readonly IFileDialogService fileDialogService;

        private ObservableCollection<string> selectedBranches;
        private string newBranch = string.Empty;
        private bool hasAlertSound = false;
        private int beforeAlertMinutes;
        private int expiredMinutes;
        private string soundPath;
        private string oldSoundPath;
        #endregion


        public SettingDialogViewModel(ISettingDialogView view, IDataService dataservice, IFileDialogService fileDialogService)
            : base(view)
        {
            this.fileDialogService = fileDialogService;

            this.submitCommand = new DelegateCommand(() => Close(true), CanSubmitSetting);
            this.cancelCommand = new DelegateCommand(() => Close(false));

            this.browseSoundFile = new DelegateCommand(BrowseSoundFileCommand);

            this.branches = dataservice.Branches;
            this.selectedBranches = new ObservableCollection<string>();

            this.addNewBranchCommand = new DelegateCommand(AddNewBranch);
            this.removeBranchCommand = new DelegateCommand(RemoveBranch, CanRemoveBranch);

            AddWeakEventListener(SelectedBranches, SelectedBranchesChanged);
        }

        #region Properties
        public ICommand SubmitCommand { get { return this.submitCommand; } }

        public ICommand CancelCommand { get { return this.cancelCommand; } }

        public ICommand AddNewBranchCommand { get { return this.addNewBranchCommand; } }

        public ICommand RemoveBranchCommand { get { return this.removeBranchCommand; } }

        public ICommand BrowseSoundFile { get { return this.browseSoundFile; } }

        [Required(ErrorMessageResourceName = "BeforeAlertMinutesMandatory", ErrorMessageResourceType = typeof(Resources))]
        [Range(1, 65535, ErrorMessageResourceName = "BeforeAlertMinutesRange", ErrorMessageResourceType = typeof(Resources))]
        public int BeforeAlertMinutes
        {
            get { return this.beforeAlertMinutes; }
            set
            {
                if (this.beforeAlertMinutes != value)
                {
                    this.beforeAlertMinutes = value;
                    this.UpdateCommands();
                    RaisePropertyChanged("BeforeAlertMinutes");
                }
            }
        }

        [Required(ErrorMessageResourceName = "ExpiredMinutesMandatory", ErrorMessageResourceType = typeof(Resources))]
        [Range(1, 65535, ErrorMessageResourceName = "ExpiredMinutesRange", ErrorMessageResourceType = typeof(Resources))]
        public int ExpiredMinutes 
        {
            get { return this.expiredMinutes; }
            set
            {
                if (this.expiredMinutes != value)
                {
                    this.expiredMinutes = value;
                    this.UpdateCommands();
                    RaisePropertyChanged("ExpiredMinutes");
                }
            }
        }

        public bool HasAlertSound 
        {
            get { return this.hasAlertSound; }
            set
            {
                if (this.hasAlertSound != value)
                {
                    this.hasAlertSound = value;
                    if (value == false)
                    {
                        this.oldSoundPath = this.soundPath;
                        this.soundPath = string.Empty;
                    }
                    else
                    {
                        this.soundPath = this.oldSoundPath;
                    }

                    this.UpdateCommands();
                    RaisePropertyChanged("HasAlertSound");
                    RaisePropertyChanged("SoundPath");
                }
            }
        }

        [Path(ErrorMessageResourceName = "SoundPathError", ErrorMessageResourceType = typeof(Resources))]
        [RequiredIf("HasAlertSound", true, ErrorMessageResourceName = "SoundPathMandatory", ErrorMessageResourceType = typeof(Resources))]
        public string SoundPath
        {
            get { return this.soundPath; }
            set
            {
                if (this.soundPath != value)
                {
                    this.soundPath = value;
                    this.UpdateCommands();
                    RaisePropertyChanged("SoundPath");
                }
            }
        }

        public bool ResetCountDownData { get; set; }

        public string NewBranch
        {
            get { return this.newBranch; }
            set
            {
                if (this.newBranch != value)
                {
                    this.newBranch = value;
                    this.UpdateCommands();
                    RaisePropertyChanged("NewBranch");
                }
            }
        }

        public ObservableCollection<string> Branches { get { return this.branches; } }

        public ObservableCollection<string> SelectedBranches 
        { 
            get 
            {
                if (this.selectedBranches == null)
                    this.selectedBranches = new ObservableCollection<string>();
                if ((!this.selectedBranches.Any()) && (this.branches.Any()))
                    this.selectedBranches.Add(this.branches.First());
                return this.selectedBranches; 
            }
            set
            {
                this.selectedBranches = value;
            }
        }
        #endregion

        private void AddNewBranch()
        {
            if (!this.branches.Contains(this.NewBranch))
            {
                this.branches.Add(this.NewBranch);
            }
            this.selectedBranches.Clear();
            this.selectedBranches.Add(this.NewBranch);
            this.NewBranch = string.Empty;

            UpdateCommands();
        }

        private bool CanRemoveBranch() { return this.SelectedBranches.Any(); }

        private void RemoveBranch()
        {
            foreach (string item in this.selectedBranches)
            {
                this.branches.Remove(item);
            }
            this.selectedBranches.Clear();

            UpdateCommands();
        }

        private bool CanSubmitSetting()
        {
            return string.IsNullOrEmpty(this.dataErrorInfoSupport.Error);
        }

        private void SelectedBranchesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCommands();
        }

        private void UpdateCommands()
        {
            this.submitCommand.RaiseCanExecuteChanged();
            this.removeBranchCommand.RaiseCanExecuteChanged();
        }

        private void BrowseSoundFileCommand()
        {
            List<FileType> FileTypes = new List<FileType>
            {
                new FileType("MP3 music file", ".mp3"),
                new FileType("Sound wave file", ".wav")
            };

            FileDialogResult result = fileDialogService.ShowOpenFileDialog(FileTypes);
            if (!string.IsNullOrWhiteSpace(result.FileName))
            {
                SoundPath = result.FileName;
            }
        }
    }
}
