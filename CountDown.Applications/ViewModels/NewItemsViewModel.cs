using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using BigEgg.Framework.Applications.Services;
using CountDown.Applications.Properties;
using CountDown.Applications.Services;
using CountDown.Applications.Views;

namespace CountDown.Applications.ViewModels
{
    [Export]
    public class NewItemsViewModel : ViewModel<INewItemsView>
    {
        #region Members
        private readonly IMessageService messageService;
        private readonly IShellService shellService;
        private readonly ObservableCollection<object> newItemViews;
        private ICommand addItemCommand;
        private object activeNewItemView;

        private bool resetCountDownData;
        private int alertBeforeMinutes;
        #endregion

        [ImportingConstructor]
        public NewItemsViewModel(INewItemsView view, IMessageService messageService, 
            IShellService shellService)
            : base(view)
        {
            this.messageService = messageService;
            this.shellService = shellService;
            this.newItemViews = new ObservableCollection<object>();
        }

        #region Properties
        public ObservableCollection<object> NewItemViews { get { return this.newItemViews; } }

        public object ActiveNewItemView
        {
            get { return this.activeNewItemView; }
            set
            {
                if (this.activeNewItemView != value)
                {
                    this.activeNewItemView = value;
                    RaisePropertyChanged("ActiveNewItemView");
                }
            }
        }

        public ICommand AddItemCommand
        {
            get { return this.addItemCommand; }
            set
            {
                if (this.addItemCommand != value)
                {
                    this.addItemCommand = value;
                    RaisePropertyChanged("AddItemCommand");
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
                    Settings.Default.ResetCountDownData = value;
                    RaisePropertyChanged("ResetCountDownData");
                }
            }
        }

        public int AlertBeforeMinutes
        {
            get { return this.alertBeforeMinutes; }
            set
            {
                if (this.alertBeforeMinutes != value)
                {
                    this.alertBeforeMinutes = value;
                    Settings.Default.DefaultAlertBeforeMinutes = value;
                    RaisePropertyChanged("AlertBeforeMinutes");
                }
            }
        }
        #endregion
    }
}
