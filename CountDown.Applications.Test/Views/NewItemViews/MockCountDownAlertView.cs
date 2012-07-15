using System.ComponentModel.Composition;
using CountDown.Applications.Views.NewItemViews;

namespace CountDown.Applications.Test.Views.NewItemViews
{
    [Export(typeof(ICountDownAlertView))]
    public class MockCountDownAlertView : MockView, ICountDownAlertView
    {
        public string Name { get; set; }
    }
}
