using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using BigEgg.Framework.Applications;
using CountDown.Applications.Domain;
using CountDown.Applications.Properties;
using CountDown.Applications.Services;
using CountDown.Applications.Views.Dialog;

namespace CountDown.Applications.ViewModels.Dialog
{
    public class AlertDialogViewModel : DialogViewModel<IAlertDialogView>
    {
        private readonly DelegateCommand okCommand;
        private readonly ObservableCollectionEx<ICountDownItem> items;

        public AlertDialogViewModel(IAlertDialogView view, ObservableCollectionEx<ICountDownItem> items)
            : base(view)
        {
            this.okCommand = new DelegateCommand(CloseCommand);
            this.items = items;
        }


        public bool HasAlertSound
        {
            get { return Settings.Default.HasAlertSound; }
        }

        public string SoundPath
        {
            get { return Settings.Default.SoundPath; }
        }

        public ICommand OKCommand { get { return this.okCommand; } }

        public ObservableCollectionEx<ICountDownItem> Items { get { return this.items; } }

        public void AddItems(IEnumerable<ICountDownItem> newItems)
        {
            foreach (ICountDownItem item in newItems)
            {
                this.items.Add(item);
            }
            RaisePropertyChanged("Items");
        }


        private void CloseCommand()
        {
            foreach (ICountDownItem item in this.items)
            {
                item.HasAlert = true;
            }
            Close(true);
        }
    }
}
