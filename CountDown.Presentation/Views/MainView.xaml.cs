using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using BigEgg.Framework.Applications;
using CountDown.Applications.ViewModels;
using CountDown.Applications.Views;

namespace CountDown.Presentation.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    [Export(typeof(IItemListView))]
    public partial class MainView : UserControl, IItemListView
    {
        private readonly Lazy<ItemListViewModel> viewModel;
        private ItemListViewModel ViewModel { get { return viewModel.Value; } }

        public MainView()
        {
            InitializeComponent();

            viewModel = new Lazy<ItemListViewModel>(() => ViewHelper.GetViewModel<ItemListViewModel>(this));
        }

    }
}
