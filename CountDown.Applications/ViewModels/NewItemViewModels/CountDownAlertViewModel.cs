using CountDown.Applications.Models;
using CountDown.Applications.Properties;
using CountDown.Applications.Views.NewItemViews;

namespace CountDown.Applications.ViewModels.NewItemViewModels
{
    public class CountDownAlertViewModel : NewItemViewModelBase<CountDownAlertModel, ICountDownAlertView>
    {
        public CountDownAlertViewModel(ICountDownAlertView view)
            : base(view, Resources.CountDownAlertName, new CountDownAlertModel())
        {
        }
    }
}
