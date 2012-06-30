using BigEgg.Framework.Applications;

namespace CountDown.Application.Views.Dialog
{
    public interface IDialogView : IView
    {
        void ShowDialog(object owner);

        void Close();
    }
}
