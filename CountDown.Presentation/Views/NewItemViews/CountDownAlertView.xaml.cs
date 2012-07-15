using System.ComponentModel.Composition;
using System.Windows.Controls;
using CountDown.Applications.Views.NewItemViews;

namespace CountDown.Presentation.Views.NewItemViews
{
    /// <summary>
    /// Interaction logic for CountDownAlertView.xaml
    /// </summary>
    [Export(typeof(ICountDownAlertView))]
    public partial class CountDownAlertView : UserControl, ICountDownAlertView
    {
        public CountDownAlertView()
        {
            InitializeComponent();
        }

        public string Name { get; set; }
    }
}
