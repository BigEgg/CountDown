using System.ComponentModel.Composition;
using CountDown.Applications.Views.NewItemViews;

namespace CountDown.Applications.Test.Views.NewItemViews
{
    [Export(typeof(IAlertAtTimeView))]
    public class MockAlertAtTimeView : MockView, IAlertAtTimeView
    {
    }
}
