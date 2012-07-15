using System.ComponentModel.Composition;
using CountDown.Applications.Views;

namespace CountDown.Applications.Test.Views
{
    [Export(typeof(INewItemsView))]
    public class MockNewItemsView : MockView, INewItemsView
    {
    }
}
