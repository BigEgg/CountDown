using System.ComponentModel;
using BigEgg.Framework.Applications;
using BigEgg.Framework.Foundation;
using CountDown.Application.Views.Dialog;

namespace CountDown.Application.ViewModels.Dialog
{
    public abstract class DialogViewModel<TView> : ViewModel<TView>, IDataErrorInfo where TView : IDialogView
    {
        protected bool? dialogResult;
        protected bool hasShow;
        protected readonly DataErrorInfoSupport dataErrorInfoSupport;


        public DialogViewModel(TView view)
            : base(view)
        {
            dataErrorInfoSupport = new DataErrorInfoSupport(this);

            hasShow = false;
        }


        public static string Title { get { return ApplicationInfo.ProductName; } }

        public bool HasShow { get { return this.hasShow; } }


        public bool? ShowDialog(object owner)
        {
            this.hasShow = true;

            ViewCore.ShowDialog(owner);
            return this.dialogResult;
        }

        protected void Close(bool? dialogResult)
        {
            this.hasShow = false;

            this.dialogResult = dialogResult;
            ViewCore.Close();
        }


        string IDataErrorInfo.Error { get { return this.dataErrorInfoSupport.Error; } }

        string IDataErrorInfo.this[string columnName] { get { return this.dataErrorInfoSupport[columnName]; } }
    }
}
