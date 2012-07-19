using System.ComponentModel;
using BigEgg.Framework.Applications;
using BigEgg.Framework.Foundation;
using BigEgg.Presentation;
using CountDown.Applications.Properties;

namespace CountDown.Applications.ViewModels.Dialogs
{
    public abstract class DialogViewModel<TView> : ViewModel<TView>, IDataErrorInfo 
        where TView : IDialogView
    {
        protected bool? dialogResult;
        protected readonly DataErrorInfoSupport dataErrorInfoSupport;


        public DialogViewModel(TView view)
            : base(view)
        {
            dataErrorInfoSupport = new DataErrorInfoSupport(this);
        }


        public string Title { get { return Resources.ApplicationName; } }


        public virtual bool? ShowDialog(object owner)
        {
            ViewCore.ShowDialog(owner);
            return this.dialogResult;
        }

        protected virtual void Close(bool? dialogResult)
        {
            this.dialogResult = dialogResult;
            ViewCore.Close();
        }


        string IDataErrorInfo.Error { get { return this.dataErrorInfoSupport.Error; } }

        string IDataErrorInfo.this[string columnName] { get { return this.dataErrorInfoSupport[columnName]; } }
    }
}
