using System;
using System.ComponentModel.Composition;
using BigEgg.Framework.Applications;
using CountDown.Application.ViewModels.Dialog;
using CountDown.Application.Views.Dialog;

namespace CountDown.Application.Test.Views.Dialogs
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
