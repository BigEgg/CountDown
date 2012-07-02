using BigEgg.Framework.Applications;

namespace CountDown.Applications.Views.Dialog
{
    public interface IDialogView : IView
    {
        void ShowDialog(object owner);

        void Close();
    }
}
