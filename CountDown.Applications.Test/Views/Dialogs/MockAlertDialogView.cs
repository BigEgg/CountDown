using System;
using System.ComponentModel.Composition;
using BigEgg.Framework.Applications;
using CountDown.Applications.ViewModels.Dialog;
using CountDown.Applications.Views.Dialog;

namespace CountDown.Applications.Test.Views.Dialogs
{
    [Export(typeof(IAlertDialogView)), Export]
    public class MockAlertDialogView : MockDialogViewBase, IAlertDialogView
    {
        public Action<MockAlertDialogView> ShowDialogAction { get; set; }
        public AlertDialogViewModel ViewModel { get { return ViewHelper.GetViewModel<AlertDialogViewModel>(this); } }


        protected override void OnShowDialogAction()
        {
            if (ShowDialogAction != null) { ShowDialogAction(this); }
        }
    }
}
