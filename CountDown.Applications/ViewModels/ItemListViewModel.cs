using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using BigEgg.Framework.Applications.Services;
using CountDown.Applications.Domain;
using CountDown.Applications.Services;
using CountDown.Applications.Views;

namespace CountDown.Applications.ViewModels
{
    [Export]
    public class ItemListViewModel : ViewModel<IItemListView>
    {
        private readonly IMessageService messageService;
        private readonly IShellService shellService;
        private readonly IDataService dataService;

        private readonly DelegateCommand deleteItemsCommand;


        [ImportingConstructor]
        public ItemListViewModel(IItemListView view, IMessageService messageService, 
            IShellService shellService, IDataService dataService)
            : base(view)
        {
            this.messageService = messageService;
            this.shellService = shellService;
            this.dataService = dataService;

            this.deleteItemsCommand = new DelegateCommand(DeleteItemsCommand, CanDeleteItemsCommand);

            AddWeakEventListener(this.dataService, DataServicePropertyChanged);
        }

        #region Properties
        public IDataService DataService { get { return this.dataService; } }

        public ICommand DeleteItems { get { return this.deleteItemsCommand; } }
        #endregion

        #region Command Methods
        private bool CanDeleteItemsCommand() { return this.dataService.SelectedItem != null; }

        private void DeleteItemsCommand()
        {
            IEnumerable<IAlertItem> itemsToExclude = this.dataService.SelectedItems.Except(new[] { this.dataService.SelectedItem });
            IAlertItem nextItem = CollectionHelper.GetNextElementOrDefault(this.dataService.Items.Except(itemsToExclude),
                this.dataService.SelectedItem);

            foreach (IAlertItem item in this.dataService.SelectedItems.ToArray())
            {
                this.dataService.Items.Remove(item);
            }

            this.dataService.SelectedItem = nextItem ?? this.dataService.Items.LastOrDefault();
        }
        #endregion

        private void DataServicePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItems")
            {
                UpdateCommands();
            }
        }

        private void UpdateCommands()
        {
            this.deleteItemsCommand.RaiseCanExecuteChanged();
        }
    }
}
