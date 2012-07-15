using CountDown.Applications.Models;
using CountDown.Applications.Properties;
using CountDown.Applications.Services;
using CountDown.Applications.Views.NewItemViews;

namespace CountDown.Applications.ViewModels.NewItemViewModels
{
    public class CountDownAlertViewModel : NewItemViewModelBase<CountDownAlertModel, ICountDownAlertView>
    {
        public CountDownAlertViewModel(ICountDownAlertView view, IDataService dataService)
            : base(view, Resources.CountDownAlertName, new CountDownAlertModel(), dataService)
        {
        }
    }
}
