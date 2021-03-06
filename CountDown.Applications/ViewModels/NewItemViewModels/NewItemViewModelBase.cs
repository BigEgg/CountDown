﻿using System.ComponentModel;
using BigEgg.Framework.Applications;
using CountDown.Applications.Models;
using CountDown.Applications.Services;
using CountDown.Applications.Views.NewItemViews;

namespace CountDown.Applications.ViewModels.NewItemViewModels
{
    public abstract class NewItemViewModelBase<TNewItemModel, TView> : ViewModel<TView>, INewItemViewModel
        where TNewItemModel : NewItemModelBase
        where TView : INewItemView
    {
        private readonly IDataService dataService;

        
        public NewItemViewModelBase(TView view, string title, TNewItemModel item, IDataService dataService)
            : base(view)
        {
            NewItem = item;
            Title = title;

            this.dataService = dataService;

            AddWeakEventListener(NewItem, NewItemPropertyChanged);
        }


        public IDataService DataService { get { return this.dataService; } }

        public TNewItemModel NewItem { get; private set; }

        public string Title { get; private set; }


        private void NewItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("NewItem");
        }
    }
}