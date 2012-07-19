using System.ComponentModel.Composition;
using System.Windows;
using BigEgg.Presentation;
using CountDown.Applications.Views.Dialogs;

namespace CountDown.Presentation.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for IAboutDialogView.xaml
    /// </summary>
    [Export(typeof(IAboutDialogView)), PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AboutDialogView : DialogWindow, IAboutDialogView
    {
        public AboutDialogView()
        {
            InitializeComponent();
        }
    }
}
