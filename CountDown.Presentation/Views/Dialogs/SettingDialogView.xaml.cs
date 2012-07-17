using System.ComponentModel.Composition;
using System.Windows;
using CountDown.Applications.Views.Dialogs;
using System;
using CountDown.Applications.ViewModels.Dialogs;
using BigEgg.Framework.Applications;

namespace CountDown.Presentation.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for SettingDialogView.xaml
    /// </summary>
    [Export(typeof(ISettingDialogView)), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SettingDialogView : Window, ISettingDialogView
    {
        private readonly Lazy<SettingDialogViewModel> viewModel;

        public SettingDialogView()
        {
            InitializeComponent();

            viewModel = new Lazy<SettingDialogViewModel>(() => ViewHelper.GetViewModel<SettingDialogViewModel>(this));
        }

        public void ShowDialog(object owner)
        {
            Owner = owner as Window;
            ShowDialog();
        }


        private SettingDialogViewModel ViewModel { get { return viewModel.Value; } }


        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            foreach (string item in e.RemovedItems)
            {
                ViewModel.SelectedBranches.Remove(item);
            }
            foreach (string item in e.AddedItems)
            {
                ViewModel.SelectedBranches.Add(item);
            }
        }
    }
}
