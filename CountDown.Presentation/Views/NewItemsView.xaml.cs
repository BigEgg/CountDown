using System.ComponentModel.Composition;
using System.Windows.Controls;
using CountDown.Applications.Views;

namespace CountDown.Presentation.Views
{
    /// <summary>
    /// Interaction logic for NewItemsView.xaml
    /// </summary>
    [Export(typeof(INewItemsView))]
    public partial class NewItemsView : UserControl, INewItemsView
    {
        public NewItemsView()
        {
            InitializeComponent();
        }
    }
}
