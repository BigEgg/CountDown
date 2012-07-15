using System.ComponentModel.Composition;
using System.Windows;
using CountDown.Applications.Views.Dialogs;

namespace CountDown.Presentation.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for IAboutDialogView.xaml
    /// </summary>
    [Export(typeof(IAboutDialogView)), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AboutDialogView : Window, IAboutDialogView
    {
        public AboutDialogView()
        {
            InitializeComponent();
        }


        public void ShowDialog(object owner)
        {
            Owner = owner as Window;
            ShowDialog();
        }
    }
}
