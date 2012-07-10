using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using CountDown.Applications.Views.Dialog;
using System.IO;
using CountDown.Applications.ViewModels.Dialog;
using BigEgg.Framework.Applications;

namespace CountDown.Presentation.Views.Dialog
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
    }
}
