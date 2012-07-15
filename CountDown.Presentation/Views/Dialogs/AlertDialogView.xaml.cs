using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Media;
using BigEgg.Framework.Applications;
using CountDown.Applications.ViewModels.Dialogs;
using CountDown.Applications.Views.Dialogs;

namespace CountDown.Presentation.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for AlertDialogView.xaml
    /// </summary>
    [Export(typeof(IAlertDialogView))]
    public partial class AlertDialogView : Window, IAlertDialogView
    {
        private readonly Lazy<AlertDialogViewModel> viewModel;
        private AlertDialogViewModel ViewModel { get { return viewModel.Value; } }
        private MediaPlayer player = null;

        public AlertDialogView()
        {
            InitializeComponent();

            viewModel = new Lazy<AlertDialogViewModel>(() => ViewHelper.GetViewModel<AlertDialogViewModel>(this));
        }


        public void ShowDialog(object owner)
        {
            Owner = owner as Window;
            Show();
            this.Visibility = Visibility.Visible;

            if (ViewModel.HasAlertSound)
            {
                if (File.Exists(ViewModel.SoundPath))
                {
                    if (this.player == null) { this.player = new MediaPlayer(); }
                    player.Stop();
                    player.Open(new Uri(ViewModel.SoundPath, UriKind.Relative));
                    player.Play();
                }
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
