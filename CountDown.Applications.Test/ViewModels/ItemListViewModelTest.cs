using System;
using CountDown.Applications.Domain;
using CountDown.Applications.Services;
using CountDown.Applications.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountDown.Applications.Test.ViewModels
{
    [TestClass]
    public class ItemListViewModelTest : TestClassBase
    {
        [TestMethod]
        public void CanDeleteCommandChangeTest()
        {
            ItemListViewModel viewModel = Container.GetExportedValue<ItemListViewModel>();
            IDataService dataService = Container.GetExportedValue<IDataService>();

            Assert.AreEqual(0, dataService.SelectedItems.Count);
            Assert.AreEqual(false, viewModel.DeleteItems.CanExecute(null));

            dataService.Items.Add(
                new AlertItem
                {
                    Time=DateTime.Now,
                    Notice="Test",
                    AlertTime=DateTime.Now.AddHours(1)
                }
            );

            Assert.AreEqual(1, dataService.SelectedItems.Count);
            Assert.AreEqual(true, viewModel.DeleteItems.CanExecute(null));

            viewModel.DeleteItems.Execute(null);
            Assert.AreEqual(0, dataService.SelectedItems.Count);
            Assert.AreEqual(false, viewModel.DeleteItems.CanExecute(null));
        }
    }
}
