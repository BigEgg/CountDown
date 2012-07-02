using System.ComponentModel.Composition;
using CountDown.Applications.Views;

namespace CountDown.Applications.Test.Views
{
    [Export(typeof(IMainView))]
    public class MockMainView : MockView, IMainView
    {
    }
}
