using System.ComponentModel.Composition;
using CountDown.Application.Views;

namespace CountDown.Application.Test.Views
{
    [Export(typeof(IMainView))]
    public class MockMainView : MockView, IMainView
    {
    }
}
