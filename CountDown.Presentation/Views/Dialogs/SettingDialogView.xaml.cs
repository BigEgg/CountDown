using System;
using System.ComponentModel.Composition;
using System.Windows;
using BigEgg.Framework.Applications;
using BigEgg.Presentation;
using CountDown.Applications.ViewModels.Dialogs;
using CountDown.Applications.Views.Dialogs;

namespace CountDown.Presentation.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for SettingDialogView.xaml
    /// </summary>
    [Export(typeof(ISettingDialogView)), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SettingDialogView : DialogWindow, ISettingDialogView
    {
        private readonly Lazy<SettingDialogViewModel> viewModel;

        public SettingDialogView()
        {
            InitializeComponent();

            viewModel = new Lazy<SettingDialogViewModel>(() => ViewHelper.GetViewModel<SettingDialogViewModel>(this));
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
