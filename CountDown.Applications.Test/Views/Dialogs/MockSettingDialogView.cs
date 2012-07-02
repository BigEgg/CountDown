using System;
using System.ComponentModel.Composition;
using BigEgg.Framework.Applications;
using CountDown.Applications.ViewModels.Dialog;
using CountDown.Applications.Views.Dialog;

namespace CountDown.Applications.Test.Views.Dialogs
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
