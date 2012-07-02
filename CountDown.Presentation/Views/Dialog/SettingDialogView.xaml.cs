using System.ComponentModel.Composition;
using System.Windows;
using CountDown.Applications.Views.Dialog;

namespace CountDown.Presentation.Views.Dialog
{
    /// <summary>
    /// Interaction logic for SettingDialogView.xaml
    /// </summary>
    [Export(typeof(ISettingDialogView)), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SettingDialogView : Window, ISettingDialogView
    {
        public SettingDialogView()
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
