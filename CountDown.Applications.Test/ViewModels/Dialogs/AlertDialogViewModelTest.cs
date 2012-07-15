using System;
using BigEgg.Framework.UnitTesting;
using CountDown.Applications.Domain;
using CountDown.Applications.Services;
using CountDown.Applications.Test.Views.Dialogs;
using CountDown.Applications.ViewModels.Dialogs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountDown.Applications.Test.ViewModels.Dialogs
{
    [TestClass]
    public class AlertDialogViewModelTest
    {
        [TestMethod]
        public void AlertDialogViewModelCloseTest()
        {
            MultiThreadingObservableCollection<IAlertItem> items = new MultiThreadingObservableCollection<IAlertItem>();
            items.Add(new AlertItem
                {
                    Time = DateTime.Now,
                    AlertTime = DateTime.Now,
                    Notice = "Test 1"
                }
            );
            items.Add(new AlertItem
                {
                    Time = DateTime.Now,
                    AlertTime = DateTime.Now,
                    Notice = "Test 2"
                }
            );
            items.Add(new AlertItem
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

        [TestMethod]
        public void PropertiesWithNotification()
        {
            MultiThreadingObservableCollection<IAlertItem> items = new MultiThreadingObservableCollection<IAlertItem>();
            MockAlertDialogView view = new MockAlertDialogView();
            AlertDialogViewModel viewModel = new AlertDialogViewModel(view, items);

            Assert.AreEqual(items, viewModel.Items);
            Assert.AreEqual(0, viewModel.Items.Count);

            bool hasAlertSound = viewModel.HasAlertSound;

            AssertHelper.PropertyChangedEvent(viewModel, x => x.HasAlertSound, () => viewModel.HasAlertSound = !hasAlertSound);
            Assert.AreEqual(!hasAlertSound, viewModel.HasAlertSound);

            MultiThreadingObservableCollection<IAlertItem> newItems = new MultiThreadingObservableCollection<IAlertItem>();
            newItems.Add(
                new AlertItem
                {
                    Time = DateTime.Now,
                    AlertTime = DateTime.Now,
                    Notice = "Test 1"
                }
            );

            AssertHelper.PropertyChangedEvent(viewModel, x => x.Items, () => viewModel.AddItems(newItems));
            Assert.AreEqual(1, viewModel.Items.Count);
        }

    }
}
