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
using CountDown.Applications.Properties;
using System.Globalization;

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
            ShellViewModel shellViewModel = Container.GetExportedValue<ShellViewModel>();
            Assert.IsNotNull(shellViewModel.AboutCommand);
            Assert.IsNotNull(shellViewModel.SettingCommand);
            Assert.IsNotNull(shellViewModel.ExitCommand);

            applicationController.Run();
            MockShellView shellView = (MockShellView)Container.GetExportedValue<IShellView>();
            Assert.IsTrue(shellView.IsVisible);

            shellViewModel.ExitCommand.Execute(null);
            Assert.IsFalse(shellView.IsVisible);

            applicationController.Shutdown();
        }

        [TestMethod]
        public void LanguageSettingsTest()
        {
            Settings.Default.Culture = "en-US";
            Settings.Default.UICulture = "en-US";

            IApplicationController applicationController = Container.GetExportedValue<IApplicationController>();

            Assert.AreEqual(new CultureInfo("en-US"), CultureInfo.CurrentCulture);
            Assert.AreEqual(new CultureInfo("en-US"), CultureInfo.CurrentUICulture);

            applicationController.Initialize();
            applicationController.Run();

            ShellViewModel shellViewModel = Container.GetExportedValue<ShellViewModel>();
            shellViewModel.ChineseCommand.Execute(null);
            Assert.AreEqual(new CultureInfo("zh-CN"), shellViewModel.NewLanguage);

            bool settingsSaved = false;
            Settings.Default.SettingsSaving += (sender, e) =>
            {
                settingsSaved = true;
            };

            applicationController.Shutdown();
            Assert.AreEqual("zh-CN", Settings.Default.UICulture);
            Assert.IsTrue(settingsSaved);
        }
    }
}
