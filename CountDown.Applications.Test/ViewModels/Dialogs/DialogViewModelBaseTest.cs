using System;
using BigEgg.Framework.Applications;
using CountDown.Applications.Test.Views.Dialogs;
using CountDown.Applications.ViewModels.Dialogs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountDown.Applications.Test.ViewModels.Dialogs
{
    [TestClass]
    public class DialogViewModelBaseTest
    {
        [TestMethod]
        public void DialogViewModelCloseTest()
        {
            MockDialogView view = new MockDialogView();
            MockDialogViewModel viewModel = new MockDialogViewModel(view);

            Assert.AreNotEqual(string.Empty, MockDialogViewModel.Title);

            object owner = new object();
            Assert.IsFalse(view.IsVisible);
            view.ShowDialogAction = v =>
            {
                Assert.AreEqual(owner, v.Owner);
                Assert.IsTrue(v.IsVisible);
            };
            bool? dialogResult = viewModel.ShowDialog(owner);
            Assert.IsNull(dialogResult);
            Assert.IsFalse(view.IsVisible);
        }


        private class MockDialogViewModel : DialogViewModel<MockDialogView>
        {
            public MockDialogViewModel(MockDialogView view)
                : base(view)
            {
            }
        }

        private class MockDialogView : MockDialogViewBase
        {
            public Action<MockDialogView> ShowDialogAction { get; set; }
            public MockDialogViewModel ViewModel { get { return ViewHelper.GetViewModel<MockDialogViewModel>(this); } }


            protected override void OnShowDialogAction()
            {
                if (ShowDialogAction != null) { ShowDialogAction(this); }
            }
        }
    }
}
