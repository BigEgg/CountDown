using System.Collections.ObjectModel;
using System.Windows.Input;
using BigEgg.Framework.Applications;
using CountDown.Applications.Domain;
using CountDown.Applications.Models;
using CountDown.Applications.Services;

namespace CountDown.Applications.Test.Services
{
    public class MockDataService : DataModel, IDataService
    {
        private ICommand newCountDownItem;
        private ICommand deleteCountDownItem;

        #region Properties
        public ObservableCollectionEx<ICountDownItem> CountDownItems
        {
            get { return new ObservableCollectionEx<ICountDownItem>(); }
        }

        public ObservableCollectionEx<ICountDownItem> AlertItems
        {
            get { return new ObservableCollectionEx<ICountDownItem>(); }
        }

        public ObservableCollection<ICountDownItem> SelectItems
        {
            get { return new ObservableCollection<ICountDownItem>(); }
        }

        public ObservableCollection<string> Branches
        {
            get { return new ObservableCollection<string>(); }
        }

        public NewCountDownModel NewCountDownModel { get { return new NewCountDownModel(); } }

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


        #region Public Methods
        public void CleanExpiredItems()
        {
        }

        public void CheckAlertItems()
        {
        }
        #endregion

    }
}
