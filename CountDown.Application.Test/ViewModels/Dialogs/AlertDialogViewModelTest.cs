using System;
using System.Collections.ObjectModel;
using CountDown.Application.Domain;
using CountDown.Application.Test.Views.Dialogs;
using CountDown.Application.ViewModels.Dialog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountDown.Application.Test.ViewModels.Dialogs
{
    [TestClass]
    public class AlertDialogViewModelTest
    {
        [TestMethod]
        public void AlertDialogViewModelCloseTest()
        {
            ObservableCollection<ICountDownItem> items = new ObservableCollection<ICountDownItem>(); 
            items.Add(new CountDownItem
                {
                    Time = DateTime.Now,
                    AlertTime = DateTime.Now,
                    Notice = "Test 1"
                }
            );
            items.Add(new CountDownItem
                {
                    Time = DateTime.Now,
                    AlertTime = DateTime.Now,
                    Notice = "Test 2"
                }
            );
            items.Add(new CountDownItem
                {
                    Time = DateTime.Now,
                    AlertTime = DateTime.Now,
                    Notice = "Test 3"
                }
            );

            Assert.AreEqual(false, items[0].HasAlert);
            Assert.AreEqual(false, items[1].HasAlert);
            Assert.AreEqual(false, items[2].HasAlert);

            MockAlertDialogView view = new MockAlertDialogView();
            AlertDialogViewModel viewModel = new AlertDialogViewModel(view, items);

            Assert.AreEqual(items, viewModel.Items);

            object owner = new object();
            view.ShowDialogAction = v =>
            {
                viewModel.OKCommand.Execute(null);
            };
            bool? dialogResult = viewModel.ShowDialog(owner);
            Assert.AreEqual(true, dialogResult);

            Assert.AreEqual(true, items[0].HasAlert);
            Assert.AreEqual(true, items[1].HasAlert);
            Assert.AreEqual(true, items[2].HasAlert);
        }
    }
}
