using BigEgg.Framework.Applications;

namespace CountDown.Applications.Views.NewItemViews
{
    public interface INewItemView : IView
    {
        string Title { get; set; }
    }
}
