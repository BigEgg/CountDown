using System.ComponentModel.Composition;
using CountDown.Applications.Views;

namespace CountDown.Applications.Test.Views
{
    [Export(typeof(IItemListView))]
    public class MockItemListViewModel : MockView, IItemListView
    {
    }
}
