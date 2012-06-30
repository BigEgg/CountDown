using System;
using System.Threading;
using System.Windows.Threading;
using BigEgg.Framework.Applications;
using BigEgg.Framework.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigEgg.Framework.Test.Applications
{
    [TestClass]
    public class ViewHelperTest
    {
        [TestMethod]
        public void GetViewModelTest()
        {
            MockView view = new MockView();
            MockViewModel viewModel = new MockViewModel(view);

            Assert.AreEqual(viewModel, view.GetViewModel<MockViewModel>());

            AssertHelper.ExpectedException<ArgumentNullException>(() => ViewHelper.GetViewModel(null));
        }

        [TestMethod]
        public void GetViewModelWithDispatcherTest()
        {
            SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());

            MockView view = new MockView();
            MockViewModel viewModel = new MockViewModel(view);

            Assert.AreEqual(viewModel, view.GetViewModel<MockViewModel>());
        }


        private class MockView : IView
        {
            public object DataContext { get; set; }
        }

        private class MockViewModel : ViewModel
        {
            public MockViewModel(IView view)
                : base(view)
            {
            }

            private static void SetDataContext(IView view, ViewModel viewModel)
            {
                view.DataContext = viewModel;
            }
        }
    }
}
