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
    public class AlartDialogViewModel : DialogViewModel<IAlartDialogView>
    {
        private readonly DelegateCommand okCommand;
        private readonly ObservableCollection<ICountDownItem> items;


        [ImportingConstructor]
        public AlartDialogViewModel(IAlartDialogView view, ObservableCollection<ICountDownItem> items)
            : base(view)
        {
            this.okCommand = new DelegateCommand(CloseCommand);
            this.items = items;
            this.AlartTime = DateTime.Now;
        }


        public ICommand OKCommand { get { return this.okCommand; } }

        public ObservableCollection<ICountDownItem> Items { get { return this.items; } }

        public DateTime AlartTime { get; private set; }


        private void CloseCommand()
        {
            foreach (ICountDownItem item in this.items)
            {
                item.HasAlart = true;
            }
            Close(true);
        }
    }
}
