using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CountDown.Applications.Controllers;
using CountDown.Applications.ViewModels;
using CountDown.Applications.Test.Views;
using CountDown.Applications.Views;
using BigEgg.Framework.Applications;

namespace CountDown.Applications.Test.Controllers
{
    [TestClass]
    public class ApplicationControllerTest : TestClassBase
    {
        [TestMethod]
        public void ControllerLifecycle()
        {
            IApplicationController applicationController = Container.GetExportedValue<IApplicationController>();

            applicationController.Initialize();
            MainViewModel mainViewModel = Container.GetExportedValue<MainViewModel>();
            Assert.IsNotNull(mainViewModel.AboutCommand);
            Assert.IsNotNull(mainViewModel.SettingCommand);
            Assert.IsNotNull(mainViewModel.ExitCommand);

            applicationController.Run();
            MockShellView shellView = (MockShellView)Container.GetExportedValue<IShellView>();
            Assert.IsTrue(shellView.IsVisible);
            ShellViewModel shellViewModel = ViewHelper.GetViewModel<ShellViewModel>(shellView);
            Assert.AreEqual(mainViewModel.View, shellViewModel.ContentView);

            mainViewModel.ExitCommand.Execute(null);
            Assert.IsFalse(shellView.IsVisible);

            applicationController.Shutdown();
        }
    }
}
