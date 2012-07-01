using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using CountDown.Application.Domain;
using CountDown.Application.Views.Dialog;
using System.Collections.ObjectModel;

namespace CountDown.Application.ViewModels.Dialog
{
    [Export]
    public class AlertDialogViewModel : DialogViewModel<IAlertDialogView>
    {
        private readonly DelegateCommand okCommand;
        private readonly ObservableCollection<ICountDownItem> items;


        [ImportingConstructor]
        public AlertDialogViewModel(IAlertDialogView view, ObservableCollection<ICountDownItem> items)
            : base(view)
        {
            this.okCommand = new DelegateCommand(CloseCommand);
            this.items = items;
            this.AlertTime = DateTime.Now;
        }


        public ICommand OKCommand { get { return this.okCommand; } }

        public ObservableCollection<ICountDownItem> Items { get { return this.items; } }

        public DateTime AlertTime { get; private set; }


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
