using System.ComponentModel.Composition;
using System.Windows.Controls;
using CountDown.Applications.Views.NewItemViews;

namespace CountDown.Presentation.Views.NewItemViews
{
    /// <summary>
    /// Interaction logic for AlertAtTimeView.xaml
    /// </summary>
    [Export(typeof(IAlertAtTimeView))]
    public partial class AlertAtTimeView : UserControl, IAlertAtTimeView
    {
        public AlertAtTimeView()
        {
            InitializeComponent();
        }

        public string Title { get; set; }
    }
}

