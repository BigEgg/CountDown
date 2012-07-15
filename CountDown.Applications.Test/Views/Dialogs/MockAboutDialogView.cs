using System;
using System.ComponentModel.Composition;
using BigEgg.Framework.Applications;
using CountDown.Applications.ViewModels.Dialogs;
using CountDown.Applications.Views.Dialogs;

namespace CountDown.Applications.Test.Views.Dialogs
{
    [Export(typeof(IAboutDialogView)), Export]
    public class MockAboutDialogView : MockDialogViewBase, IAboutDialogView
    {
        public Action<MockAboutDialogView> ShowDialogAction { get; set; }
        public AboutDialogViewModel ViewModel { get { return ViewHelper.GetViewModel<AboutDialogViewModel>(this); } }


        protected override void OnShowDialogAction()
        {
            if (ShowDialogAction != null) { ShowDialogAction(this); }
        }
    }
}
