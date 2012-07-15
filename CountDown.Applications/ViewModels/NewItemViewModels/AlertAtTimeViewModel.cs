using CountDown.Applications.Models;
using CountDown.Applications.Properties;
using CountDown.Applications.Views.NewItemViews;

namespace CountDown.Applications.ViewModels.NewItemViewModels
{
    public class AlertAtTimeViewModel : NewItemViewModelBase<AlertAtTimeModel, IAlertAtTimeView>
    {
        public AlertAtTimeViewModel(IAlertAtTimeView view)
            : base(view, Resources.AlertAtTimeName, new AlertAtTimeModel())
        {
        }
    }
}