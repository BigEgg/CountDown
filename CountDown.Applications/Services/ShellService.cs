using System.ComponentModel.Composition;
using BigEgg.Framework.Applications;

namespace CountDown.Applications.Services
{
    [Export(typeof(IShellService)), Export]
    internal class ShellService : DataModel, IShellService
    {
        private object shellView;
        private object itemListView;
        private object newItemsView;

        public object ShellView
        {
            get { return this.shellView; }
            set
            {
                if (this.shellView != value)
                {
                    this.shellView = value;
                    RaisePropertyChanged("ShellView");
                }
            }
        }

        public object ItemListView
        {
            get { return this.itemListView; }
            set
            {
                if (this.itemListView != value)
                {
                    this.itemListView = value;
                    RaisePropertyChanged("ItemListView");
                }
            }
        }

        public object NewItemsView
        {
            get { return this.newItemsView; }
            set
            {
                if (this.newItemsView != value)
                {
                    this.newItemsView = value;
                    RaisePropertyChanged("NewItemsView");
                }
            }
        }
    }
}
