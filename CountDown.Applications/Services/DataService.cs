using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using CountDown.Applications.Domain;
using CountDown.Applications.Models;
using CountDown.Applications.Properties;

namespace CountDown.Applications.Services
{
    [Export(typeof(IDataService)), Export]
    internal class DataService : DataModel, IDataService
    {
        #region Members
        private readonly ObservableCollectionEx<ICountDownItem> countDownItems;
        private readonly ObservableCollectionEx<ICountDownItem> alertItems;

        private readonly ObservableCollection<string> branches;
        private readonly ObservableCollection<ICountDownItem> selectItems;
        private readonly NewCountDownModel newCountDownModel;
        private ICommand newCountDownItem;
        private ICommand deleteCountDownItem;
        #endregion

        [ImportingConstructor]
        public DataService()
        {
            this.countDownItems = new ObservableCollectionEx<ICountDownItem>();
            this.alertItems = new ObservableCollectionEx<ICountDownItem>();

            this.branches = new ObservableCollection<string>();
            this.selectItems = new ObservableCollection<ICountDownItem>();
            this.newCountDownModel = new NewCountDownModel
            {
                Days = 0,
                Hours = 0,
                Minutes = 1,
                NoticeBranch = string.Empty,
                Notice = string.Empty,
                BeforeAlertMinutes = Settings.Default.DefautBeforeAlertMinutes
            };

            AddWeakEventListener(this.selectItems, SelectItemsChanged);
            AddWeakEventListener(this.newCountDownModel, NewCountDownModelPropertyChanged);
            AddWeakEventListener(this.alertItems, AlertItemsChanged);
        }


        #region Properties
        public ObservableCollectionEx<ICountDownItem> CountDownItems 
        { 
            get 
            {
                if (this.countDownItems.Any())
                {
                    this.countDownItems.OrderBy(i => i.Time);
                    this.countDownItems.ToArray();
                }
                return this.countDownItems; 
            } 
        }

        public ObservableCollectionEx<ICountDownItem> AlertItems
        {
            get
            {
                if (this.alertItems.Any())
                {
                    this.alertItems.OrderBy(i => i.Time);
                    this.alertItems.ToArray();
                }
                return this.alertItems;
            }
        }

        public ObservableCollection<ICountDownItem> SelectItems { get { return this.selectItems; } }

        public ObservableCollection<string> Branches { get { return this.branches; } }

        public NewCountDownModel NewCountDownModel { get { return this.newCountDownModel; } }

        public ICommand NewCountDownItem
        {
            get { return this.newCountDownItem; }
            set
            {
                if (this.newCountDownItem != value)
                {
                    this.newCountDownItem = value;
                    RaisePropertyChanged("NewCountDownItem");
                }
            }
        }

        public ICommand DeleteCountDownItem
        {
            get { return this.deleteCountDownItem; }
            set
            {
                if (this.deleteCountDownItem != value)
                {
                    this.deleteCountDownItem = value;
                    RaisePropertyChanged("DeleteCountDownItem");
                }
            }
        }
        #endregion


        #region Private Methods
        private void SelectItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach(ICountDownItem item in e.NewItems)
                {
                    if (!this.CountDownItems.Contains(item)) { throw new Exception("Select item must contain in the items list."); }
                }
            }
            RaisePropertyChanged("SelectItems");
        }

        private void NewCountDownModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("NewCountDownModel");
        }

        private void AlertItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                RaisePropertyChanged("AlertItems");
            }
        }
        #endregion
    }
}
