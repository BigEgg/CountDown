using System.ComponentModel;
using BigEgg.Framework.Applications;
using BigEgg.Framework.Foundation;
using CountDown.Application.Views.Dialog;

namespace CountDown.Application.ViewModels.Dialog
{
    public abstract class DialogViewModel<TView> : ViewModel<TView>, IDataErrorInfo where TView : IDialogView
    {
        private bool? dialogResult;
        private readonly DataErrorInfoSupport dataErrorInfoSupport;


        public DialogViewModel(TView view)
            : base(view)
        {
            dataErrorInfoSupport = new DataErrorInfoSupport(this);
        }


        public static string Title { get { return ApplicationInfo.ProductName; } }


        public bool? ShowDialog(object owner)
        {
            ViewCore.ShowDialog(owner);
            return this.dialogResult;
        }

        protected void Close(bool? dialogResult)
        {
            this.dialogResult = dialogResult;
            ViewCore.Close();
        }


        string IDataErrorInfo.Error { get { return dataErrorInfoSupport.Error; } }

        string IDataErrorInfo.this[string columnName] { get { return dataErrorInfoSupport[columnName]; } }
    }
}
