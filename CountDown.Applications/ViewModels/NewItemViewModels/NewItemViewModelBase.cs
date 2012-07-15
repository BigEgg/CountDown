using System.ComponentModel;
using BigEgg.Framework.Applications;
using CountDown.Applications.Models;
using CountDown.Applications.Views.NewItemViews;

namespace CountDown.Applications.ViewModels.NewItemViewModels
{
    public abstract class NewItemViewModelBase<TNewItemModel, TView> : ViewModel<TView>, INewItemViewModel
        where TNewItemModel : NewItemModelBase
        where TView : INewItemView
    {
        public NewItemViewModelBase(TView view, string name, TNewItemModel item)
            : base(view)
        {
            NewItem = item;
            Name = name;

            AddWeakEventListener(NewItem, NewItemPropertyChanged);
        }


        public TNewItemModel NewItem { get; private set; }

        public string Name { get; private set; }


        private void NewItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("NewItem");
        }
    }
}