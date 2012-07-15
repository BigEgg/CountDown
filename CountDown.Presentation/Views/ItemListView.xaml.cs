using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using BigEgg.Framework.Applications;
using CountDown.Applications.ViewModels;
using CountDown.Applications.Views;
using CountDown.Applications.Domain;

namespace CountDown.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ItemListView.xaml
    /// </summary>
    [Export(typeof(IItemListView))]
    public partial class ItemListView : UserControl, IItemListView
    {
        private readonly Lazy<ItemListViewModel> viewModel;

        public ItemListView()
        {
            InitializeComponent();

            viewModel = new Lazy<ItemListViewModel>(() => ViewHelper.GetViewModel<ItemListViewModel>(this));
        }


        private ItemListViewModel ViewModel { get { return viewModel.Value; } }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (IAlertItem item in e.RemovedItems)
            {
                ViewModel.DataService.SelectedItems.Remove(item);
            }
            foreach (IAlertItem item in e.AddedItems)
            {
                ViewModel.DataService.SelectedItems.Add(item);
            }
        }
    }
}
