using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using BigEgg.Framework.Applications;
using CountDown.Applications.Domain;

namespace CountDown.Applications.Services
{
    [Export(typeof(IDataService)), Export]
    internal class DataService : DataModel, IDataService
    {
        #region Members
        private readonly MultiThreadingObservableCollection<IAlertItem> items;
        private readonly MultiThreadingObservableCollection<IAlertItem> alertedItems;
        private readonly ObservableCollection<string> branches;
        private IAlertItem selectedItem;
        private ObservableCollection<IAlertItem> selectedItems;
        #endregion

        [ImportingConstructor]
        public DataService()
        {
            this.items = new MultiThreadingObservableCollection<IAlertItem>();
            this.alertedItems = new MultiThreadingObservableCollection<IAlertItem>();
            this.selectedItems = new ObservableCollection<IAlertItem>();

            this.branches = new ObservableCollection<string>();

            AddWeakEventListener(this.items, ItemsChanged);
            AddWeakEventListener(this.selectedItems, SelectedItemsChanged);
            AddWeakEventListener(this.alertedItems, AlertedItemsChanged);
        }


        #region Properties
        public MultiThreadingObservableCollection<IAlertItem> Items 
        { 
            get 
            {
                return this.items; 
            } 
        }

        public MultiThreadingObservableCollection<IAlertItem> AlertedItems
        {
            get
            {
                return this.alertedItems;
            }
        }

        public ObservableCollection<IAlertItem> SelectedItems { get { return this.selectedItems; } }

        public IAlertItem SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (this.selectedItem != value)
                {
                    this.selectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }

        }

        public ObservableCollection<string> Branches { get { return this.branches; } }
        #endregion


        #region Private Methods
        private void ItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if ((e.Action == NotifyCollectionChangedAction.Add) && (!this.selectedItems.Any()))
            {
                this.SelectedItem = e.NewItems[e.NewItems.Count - 1] as IAlertItem;
            }
            RaisePropertyChanged("Items");
        }

        private void SelectedItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach(IAlertItem item in e.NewItems)
                {
                    if (!this.Items.Contains(item)) { throw new Exception("Select item must contain in the items list."); }
                }
            }
            RaisePropertyChanged("SelectedItems");
        }

        private void AlertedItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                RaisePropertyChanged("AlertedItems");
            }
        }
        #endregion
    }
}
