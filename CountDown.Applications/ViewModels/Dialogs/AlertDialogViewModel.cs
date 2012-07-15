using System.Collections.Generic;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using CountDown.Applications.Domain;
using CountDown.Applications.Properties;
using CountDown.Applications.Services;
using CountDown.Applications.Views.Dialogs;

namespace CountDown.Applications.ViewModels.Dialogs
{
    public class AlertDialogViewModel : DialogViewModel<IAlertDialogView>
    {
        private readonly DelegateCommand okCommand;
        private readonly MultiThreadingObservableCollection<IAlertItem> items;

        public AlertDialogViewModel(IAlertDialogView view, MultiThreadingObservableCollection<IAlertItem> items)
            : base(view)
        {
            this.okCommand = new DelegateCommand(CloseCommand);
            this.items = items;
        }


        public bool HasAlertSound
        {
            get { return Settings.Default.HasAlertSound; }
            set
            {
                if (Settings.Default.HasAlertSound != value)
                {
                    Settings.Default.HasAlertSound = value;
                    RaisePropertyChanged("HasAlertSound");
                }
            }
        }

        public string SoundPath { get { return Settings.Default.SoundPath; } }

        public ICommand OKCommand { get { return this.okCommand; } }

        public MultiThreadingObservableCollection<IAlertItem> Items { get { return this.items; } }

        public void AddItems(IEnumerable<IAlertItem> newItems)
        {
            foreach (IAlertItem item in newItems)
            {
                this.items.Add(item);
            }
            RaisePropertyChanged("Items");
        }


        private void CloseCommand()
        {
            foreach (IAlertItem item in this.items)
            {
                item.HasAlert = true;
            }
            Close(true);
        }
    }
}
