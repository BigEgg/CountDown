using System;
using System.Threading;
using System.Windows.Threading;
using BigEgg.Framework.Applications;
using BigEgg.Framework.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigEgg.Framework.Test.Applications
{
    [TestClass]
    public class ViewModelTest
    {
        [TestMethod]
        public void GetViewTest()
        {
            IView view = new MockView();
            DummyViewModel viewModel = new DummyViewModel(view);

            Assert.AreEqual(view, viewModel.View);
        }



        private class DummyViewModel : ViewModel
        {
            public DummyViewModel(IView view)
                : base(view)
            {
            }
        }


        private class MockView : IView
        {
            public object DataContext { get; set; }
        }
    }
}
