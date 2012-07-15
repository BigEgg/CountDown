using System.Windows.Input;
using BigEgg.Framework.Applications;
using BigEgg.Framework.UnitTesting;
using CountDown.Applications.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CountDown.Applications.Test.Views;
using CountDown.Applications.Views;

namespace CountDown.Applications.Test.ViewModels
{
    [TestClass]
    public class NewItemsViewModelTest : TestClassBase
    {
        [TestMethod]
        public void PropertiesWithNotification()
        {
            INewItemsView view = Container.GetExportedValue<INewItemsView>();
            NewItemsViewModel viewModel = Container.GetExportedValue<NewItemsViewModel>();

            object mockView1 = new object();
            AssertHelper.PropertyChangedEvent(viewModel, x => x.ActiveNewItemView, () => viewModel.ActiveNewItemView = mockView1);
            Assert.AreEqual(mockView1, viewModel.ActiveNewItemView);

            ICommand addItemCommand = new DelegateCommand(() => { });
            AssertHelper.PropertyChangedEvent(viewModel, x => x.AddItemCommand, () => viewModel.AddItemCommand = addItemCommand);
            Assert.AreEqual(addItemCommand, viewModel.AddItemCommand);

            bool resetCountDownData = viewModel.ResetCountDownData;
            AssertHelper.PropertyChangedEvent(viewModel, x => x.ResetCountDownData, () => viewModel.ResetCountDownData = !resetCountDownData);
            Assert.AreEqual(!resetCountDownData, viewModel.ResetCountDownData);

            int alertBeforeMinutes = viewModel.AlertBeforeMinutes;
            AssertHelper.PropertyChangedEvent(viewModel, x => x.AlertBeforeMinutes, () => viewModel.AlertBeforeMinutes = alertBeforeMinutes + 5);
            Assert.AreEqual(alertBeforeMinutes + 5, viewModel.AlertBeforeMinutes);
        }
    }
}
