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
        private readonly ObservableCollection<IAlertItem> selectItems;

        private readonly MultiThreadingObservableCollection<IAlertItem> alertedItems;

        private readonly ObservableCollection<string> branches;
        #endregion

        [ImportingConstructor]
        public DataService()
        {
            this.items = new MultiThreadingObservableCollection<IAlertItem>();
            this.alertedItems = new MultiThreadingObservableCollection<IAlertItem>();
            this.selectItems = new ObservableCollection<IAlertItem>();

            this.branches = new ObservableCollection<string>();

            AddWeakEventListener(this.items, ItemsChanged);
            AddWeakEventListener(this.selectItems, SelectItemsChanged);
            AddWeakEventListener(this.alertedItems, AlertItemsChanged);
        }


        #region Properties
        public MultiThreadingObservableCollection<IAlertItem> Items 
        { 
            get 
            {
                if (this.items.Any())
                {
                    this.items.OrderBy(i => i.Time);
                    this.items.ToArray();
                }
                return this.items; 
            } 
        }

        public MultiThreadingObservableCollection<IAlertItem> AlertedItems
        {
            get
            {
                if (this.alertedItems.Any())
                {
                    this.alertedItems.OrderBy(i => i.Time);
                    this.alertedItems.ToArray();
                }
                return this.alertedItems;
            }
        }

        public ObservableCollection<IAlertItem> SelectItems { get { return this.selectItems; } }

        public ObservableCollection<string> Branches { get { return this.branches; } }
        #endregion


        #region Private Methods
        private void ItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if ((e.Action == NotifyCollectionChangedAction.Add) && (!this.selectItems.Any()))
            {
                foreach (IAlertItem item in e.NewItems)
                {
                    SelectItems.Add(item);
                }
            }
        }

        private void SelectItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach(IAlertItem item in e.NewItems)
                {
                    if (!this.Items.Contains(item)) { throw new Exception("Select item must contain in the items list."); }
                }
            }
            RaisePropertyChanged("SelectItems");
        }

        private void AlertItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                RaisePropertyChanged("AlertedItems");
            }
        }
        #endregion
    }
}
