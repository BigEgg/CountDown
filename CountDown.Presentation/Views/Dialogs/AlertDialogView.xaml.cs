using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Media;
using BigEgg.Framework.Applications;
using BigEgg.Presentation;
using CountDown.Applications.ViewModels.Dialogs;
using CountDown.Applications.Views.Dialogs;

namespace CountDown.Presentation.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for AlertDialogView.xaml
    /// </summary>
    [Export(typeof(IAlertDialogView))]
    public partial class AlertDialogView : DialogWindow, IAlertDialogView
    {
        private readonly Lazy<AlertDialogViewModel> viewModel;
        private AlertDialogViewModel ViewModel { get { return viewModel.Value; } }
        private MediaPlayer player = null;

        public AlertDialogView()
        {
            InitializeComponent();

            viewModel = new Lazy<AlertDialogViewModel>(() => ViewHelper.GetViewModel<AlertDialogViewModel>(this));
        }


        public override void ShowDialog(object owner)
        {
            Show();
            Owner = owner as Window;
            this.Visibility = Visibility.Visible;

            if (File.Exists(ViewModel.SoundPath))
            {
                if (ViewModel.HasAlertSound)
                {
                    if (this.player == null) { this.player = new MediaPlayer(); }
                    player.Stop();
                    player.Open(new Uri(ViewModel.SoundPath, UriKind.Relative));
                    player.Play();
                }
            }
            else
            {
                isPlaySound.IsChecked = false;
                isPlaySound.IsEnabled = false;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;

            if (this.player != null)
                player.Stop();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.player != null)
                player.Stop();
        }
    }
}
