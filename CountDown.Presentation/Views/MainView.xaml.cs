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
    [Export(typeof(IMainView))]
    public partial class MainView : UserControl, IMainView
    {
        private readonly Lazy<MainViewModel> viewModel;
        private MainViewModel ViewModel { get { return viewModel.Value; } }

        public MainView()
        {
            InitializeComponent();

            viewModel = new Lazy<MainViewModel>(() => ViewHelper.GetViewModel<MainViewModel>(this));
        }

    }
}
