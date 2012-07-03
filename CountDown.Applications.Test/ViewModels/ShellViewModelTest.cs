using BigEgg.Framework.Applications;
using BigEgg.Framework.UnitTesting;
using CountDown.Applications.Properties;
using CountDown.Applications.Services;
using CountDown.Applications.Test.Services;
using CountDown.Applications.Test.Views;
using CountDown.Applications.ViewModels;
using CountDown.Applications.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountDown.Applications.Test.ViewModels
{
    [TestClass]
    public class ShellViewModelTest : TestClassBase
    {
        [TestMethod]
        public void ShowAndClose()
        {
            MockMessageService messageService = Container.GetExportedValue<MockMessageService>();
            MockShellView shellView = (MockShellView)Container.GetExportedValue<IShellView>();
            ShellViewModel shellViewModel = Container.GetExportedValue<ShellViewModel>();
            MainViewModel mainViewModel = Container.GetExportedValue<MainViewModel>();

            // Show the ShellView
            Assert.IsFalse(shellView.IsVisible);
            shellViewModel.Show();
            Assert.IsTrue(shellView.IsVisible);

            Assert.AreNotEqual("", shellViewModel.Title);

            // Try to close the ShellView but cancel this operation through the closing event
            bool cancelClosing = true;
            shellViewModel.Closing += (sender, e) =>
            {
                e.Cancel = cancelClosing;
            };
            shellViewModel.Close();
            Assert.IsTrue(shellView.IsVisible);

            // Close the ShellView via the ExitCommand
            cancelClosing = false;
            AssertHelper.PropertyChangedEvent(mainViewModel, x => x.ExitCommand, () =>
                mainViewModel.ExitCommand = new DelegateCommand(() => shellViewModel.Close()));
            mainViewModel.ExitCommand.Execute(null);
            Assert.IsFalse(shellView.IsVisible);
        }

        [TestMethod]
        public void RestoreWindowLocationAndSize()
        {
            MockPresentationService presentationService = (MockPresentationService)Container.GetExportedValue<IPresentationService>();
            presentationService.VirtualScreenWidth = 1000;
            presentationService.VirtualScreenHeight = 700;

            SetSettingsValues(20, 10, 400, 300, true);

            ShellViewModel shellViewModel = Container.GetExportedValue<ShellViewModel>();
            MockShellView shellView = (MockShellView)Container.GetExportedValue<IShellView>();
            Assert.AreEqual(20, shellView.Left);
            Assert.AreEqual(10, shellView.Top);
            Assert.AreEqual(400, shellView.Width);
            Assert.AreEqual(300, shellView.Height);
            Assert.IsTrue(shellView.IsMaximized);

            shellView.Left = 25;
            shellView.Top = 15;
            shellView.Width = 450;
            shellView.Height = 350;
            shellView.IsMaximized = false;

            shellView.Close();
            AssertSettingsValues(25, 15, 450, 350, false);
        }

        [TestMethod]
        public void RestoreWindowLocationAndSizeSpecial()
        {
            MockDataService dataService = new MockDataService();
            MockPresentationService presentationService = (MockPresentationService)Container.GetExportedValue<IPresentationService>();
            presentationService.VirtualScreenWidth = 1000;
            presentationService.VirtualScreenHeight = 700;

            MockShellView shellView = (MockShellView)Container.GetExportedValue<IShellView>();
            IShellService shellService = Container.GetExportedValue<IShellService>();
            shellView.SetNAForLocationAndSize();

            SetSettingsValues();
            new ShellViewModel(shellView, dataService, presentationService, shellService).Close();
            AssertSettingsValues(double.NaN, double.NaN, double.NaN, double.NaN, false);

            // Height is 0 => don't apply the Settings values
            SetSettingsValues(0, 0, 1, 0);
            new ShellViewModel(shellView, dataService, presentationService, shellService).Close();
            AssertSettingsValues(double.NaN, double.NaN, double.NaN, double.NaN, false);

            // Left = 100 + Width = 901 > VirtualScreenWidth = 1000 => don't apply the Settings values
            SetSettingsValues(100, 100, 901, 100);
            new ShellViewModel(shellView, dataService, presentationService, shellService).Close();
            AssertSettingsValues(double.NaN, double.NaN, double.NaN, double.NaN, false);

            // Top = 100 + Height = 601 > VirtualScreenWidth = 600 => don't apply the Settings values
            SetSettingsValues(100, 100, 100, 601);
            new ShellViewModel(shellView, dataService, presentationService, shellService).Close();
            AssertSettingsValues(double.NaN, double.NaN, double.NaN, double.NaN, false);

            // Use the limit values => apply the Settings values
            SetSettingsValues(0, 0, 1000, 700);
            new ShellViewModel(shellView, dataService, presentationService, shellService).Close();
            AssertSettingsValues(0, 0, 1000, 700, false);
        }


        private void SetSettingsValues(double left = 0, double top = 0, double width = 0, double height = 0, bool isMaximized = false)
        {
            Settings.Default.Left = left;
            Settings.Default.Top = top;
            Settings.Default.Width = width;
            Settings.Default.Height = height;
            Settings.Default.IsMaximized = isMaximized;
        }

        private void AssertSettingsValues(double left, double top, double width, double height, bool isMaximized)
        {
            Assert.AreEqual(left, Settings.Default.Left);
            Assert.AreEqual(top, Settings.Default.Top);
            Assert.AreEqual(width, Settings.Default.Width);
            Assert.AreEqual(height, Settings.Default.Height);
            Assert.AreEqual(isMaximized, Settings.Default.IsMaximized);
        }
    }
}
