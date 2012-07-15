using BigEgg.Framework.UnitTesting;
using CountDown.Applications.Models;
using CountDown.Applications.Test.Views;
using CountDown.Applications.ViewModels.NewItemViewModels;
using CountDown.Applications.Views.NewItemViews;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CountDown.Applications.Services;

namespace CountDown.Applications.Test.ViewModels.NewItemViewModels
{
    [TestClass]
    public class NewItemViewModelBaseTest
    {
        [TestMethod]
        public void GeneralNewItemViewModelBaseTest()
        {
            MockNewItemView view = new MockNewItemView();
            DataService dataService = new DataService();
            MockNewItemViewModel viewModel = new MockNewItemViewModel(view, dataService);

            Assert.AreEqual("MockNewItemViewModel", viewModel.Name);

            Assert.AreEqual(typeof(MockNewItemModel), viewModel.NewItem.GetType());
        }

        [TestMethod]
        public void PropertiesWithNotification()
        {
            MockNewItemView view = new MockNewItemView();
            DataService dataService = new DataService();
            MockNewItemViewModel viewModel = new MockNewItemViewModel(view, dataService);

            string branch = "Test Branch";
            AssertHelper.PropertyChangedEvent(viewModel, x => x.NewItem, () => viewModel.NewItem.NoticeBranch = branch);
            Assert.AreEqual(branch, viewModel.NewItem.NoticeBranch);
        }


        private class MockNewItemViewModel : NewItemViewModelBase<MockNewItemModel, MockNewItemView>
        {
            public MockNewItemViewModel(MockNewItemView view, IDataService dataService)
                : base(view, "MockNewItemViewModel", new MockNewItemModel(), dataService)
            {
            }
        }

        private class MockNewItemModel : NewItemModelBase
        {
        }

        private class MockNewItemView : MockView, INewItemView
        {
            public string Name { get; set; }
        }
    }
}
