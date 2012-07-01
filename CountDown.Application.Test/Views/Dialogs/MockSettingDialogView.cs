using System;
using System.ComponentModel.Composition;
using BigEgg.Framework.Applications;
using CountDown.Application.ViewModels.Dialog;
using CountDown.Application.Views.Dialog;

namespace CountDown.Application.Test.Views.Dialogs
{
    [Export(typeof(ISettingDialogView)), Export]
    public class MockSettingDialogView : MockDialogViewBase, ISettingDialogView
    {
        public Action<MockSettingDialogView> ShowDialogAction { get; set; }
        public SettingDialogViewModel ViewModel { get { return ViewHelper.GetViewModel<SettingDialogViewModel>(this); } }


        protected override void OnShowDialogAction()
        {
            if (ShowDialogAction != null) { ShowDialogAction(this); }
        }
    }
}
