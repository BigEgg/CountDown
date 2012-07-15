using BigEgg.Framework.Applications;

namespace CountDown.Applications.Views.Dialogs
{
    public interface IDialogView : IView
    {
        void ShowDialog(object owner);

        void Close();
    }
}
