using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CountDown.Application.Views.Dialog;
using System.ComponentModel.Composition;
using CountDown.Application.ViewModels.Dialog;
using BigEgg.Framework.Applications;

namespace CountDown.Application.Test.Views.Dialogs
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
