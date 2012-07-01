using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CountDown.Application.ViewModels.Dialog;
using CountDown.Application.Test.Views.Dialogs;
using CountDown.Application.Properties;
using BigEgg.Framework.Foundation;

namespace CountDown.Application.Test.ViewModels.Dialogs
{
    [TestClass]
    public class SettingDialogViewModelTest : TestClassBase
    {
        private int beforeAlertMinutes;
        private int expiredMinutes;
        private bool hasAlertSound;
        private string soundPath;
        private bool resetCountDownData;

        protected override void OnTestInitialize()
        {
            this.beforeAlertMinutes = Settings.Default.DefautBeforeAlertMinutes;
            this.expiredMinutes = Settings.Default.DefaultExpiredMinutes;
            this.hasAlertSound = Settings.Default.HasAlertSound;
            this.soundPath = Settings.Default.SoundPath;
            this.resetCountDownData = Settings.Default.ResetCountDownData;
        }

        protected override void OnTestCleanup()
        {
            Settings.Default.DefautBeforeAlertMinutes = this.beforeAlertMinutes;
            Settings.Default.DefaultExpiredMinutes = this.expiredMinutes;
            Settings.Default.HasAlertSound = this.hasAlertSound;
            Settings.Default.SoundPath = this.soundPath;
            Settings.Default.ResetCountDownData = this.resetCountDownData;
            Settings.Default.Save();
        }

        [TestMethod]
        public void SettingDialogViewModelCloseTest()
        {
            SettingDialogViewModel viewModel = Container.GetExportedValue<SettingDialogViewModel>();

            Assert.AreEqual(this.beforeAlertMinutes, viewModel.BeforeAlertMinutes);
            Assert.AreEqual(this.expiredMinutes, viewModel.ExpiredMinutes);
            Assert.AreEqual(this.hasAlertSound, viewModel.HasAlertSound);
            Assert.AreEqual(this.soundPath, viewModel.SoundPath);
            Assert.AreEqual(this.resetCountDownData, viewModel.ResetCountDownData);

            viewModel.BeforeAlertMinutes += 1;
            viewModel.ExpiredMinutes += 1;
            viewModel.HasAlertSound = !viewModel.HasAlertSound;
            viewModel.SoundPath = "C:\\Music.mp3";
            viewModel.ResetCountDownData = !viewModel.ResetCountDownData;
            viewModel.SubmitCommand.Execute(null);

            Assert.AreEqual(this.beforeAlertMinutes + 1, viewModel.BeforeAlertMinutes);
            Assert.AreEqual(this.expiredMinutes + 1, viewModel.ExpiredMinutes);
            Assert.AreEqual(!this.hasAlertSound, viewModel.HasAlertSound);
            Assert.AreEqual("C:\\Music.mp3", viewModel.SoundPath);
            Assert.AreEqual(!this.resetCountDownData, viewModel.ResetCountDownData);

            Assert.AreEqual(this.beforeAlertMinutes + 1, Settings.Default.DefautBeforeAlertMinutes);
            Assert.AreEqual(this.expiredMinutes + 1, Settings.Default.DefaultExpiredMinutes);
            Assert.AreEqual(!this.hasAlertSound, Settings.Default.HasAlertSound);
            Assert.AreEqual("C:\\Music.mp3", Settings.Default.SoundPath);
            Assert.AreEqual(!this.resetCountDownData, Settings.Default.ResetCountDownData);
        }

        [TestMethod]
        public void SettingDialogViewModelBeforeAlertMinutesValidationTest()
        {
            SettingDialogViewModel viewModel = Container.GetExportedValue<SettingDialogViewModel>();

            Assert.AreEqual(this.beforeAlertMinutes, viewModel.BeforeAlertMinutes);
            Assert.AreEqual("", viewModel.Validate("BeforeAlertMinutes"));

            viewModel.BeforeAlertMinutes = 1;
            Assert.AreEqual(1, viewModel.BeforeAlertMinutes);
            Assert.AreEqual("", viewModel.Validate("BeforeAlertMinutes"));

            viewModel.BeforeAlertMinutes = 100;
            Assert.AreEqual(100, viewModel.BeforeAlertMinutes);
            Assert.AreEqual("", viewModel.Validate("BeforeAlertMinutes"));

            viewModel.BeforeAlertMinutes = 65535;
            Assert.AreEqual(65535, viewModel.BeforeAlertMinutes);
            Assert.AreEqual("", viewModel.Validate("BeforeAlertMinutes"));

            viewModel.BeforeAlertMinutes = 0;
            Assert.AreEqual(0, viewModel.BeforeAlertMinutes);
            Assert.AreNotEqual("", viewModel.Validate("BeforeAlertMinutes"));

            viewModel.BeforeAlertMinutes = 65536;
            Assert.AreEqual(65536, viewModel.BeforeAlertMinutes);
            Assert.AreNotEqual("", viewModel.Validate("BeforeAlertMinutes"));
        }

        [TestMethod]
        public void SettingDialogViewModelExpiredMinutesValidationTest()
        {
            SettingDialogViewModel viewModel = Container.GetExportedValue<SettingDialogViewModel>();

            Assert.AreEqual(this.expiredMinutes, viewModel.ExpiredMinutes);
            Assert.AreEqual("", viewModel.Validate("ExpiredMinutes"));

            viewModel.ExpiredMinutes = 1;
            Assert.AreEqual(1, viewModel.ExpiredMinutes);
            Assert.AreEqual("", viewModel.Validate("ExpiredMinutes"));

            viewModel.ExpiredMinutes = 100;
            Assert.AreEqual(100, viewModel.ExpiredMinutes);
            Assert.AreEqual("", viewModel.Validate("ExpiredMinutes"));

            viewModel.ExpiredMinutes = 65535;
            Assert.AreEqual(65535, viewModel.ExpiredMinutes);
            Assert.AreEqual("", viewModel.Validate("ExpiredMinutes"));

            viewModel.ExpiredMinutes = 0;
            Assert.AreEqual(0, viewModel.ExpiredMinutes);
            Assert.AreNotEqual("", viewModel.Validate("ExpiredMinutes"));

            viewModel.ExpiredMinutes = 65536;
            Assert.AreEqual(65536, viewModel.ExpiredMinutes);
            Assert.AreNotEqual("", viewModel.Validate("ExpiredMinutes"));
        }

        [TestMethod]
        public void SettingDialogViewModelHasAlertSoundChangeTest()
        {
            SettingDialogViewModel viewModel = Container.GetExportedValue<SettingDialogViewModel>();

            Assert.AreEqual(this.hasAlertSound, viewModel.HasAlertSound);

            if (!this.hasAlertSound)
            {
                viewModel.HasAlertSound = true;
            }
            viewModel.SoundPath = "C:\\Music.mp3";

            Assert.AreEqual(true, viewModel.HasAlertSound);
            Assert.AreEqual("C:\\Music.mp3", viewModel.SoundPath);

            viewModel.HasAlertSound = false;
            Assert.AreEqual(false, viewModel.HasAlertSound);
            Assert.AreEqual(string.Empty, viewModel.SoundPath);
        }

        [TestMethod]
        public void SettingDialogViewModelSoundPathValidationTest()
        {
            SettingDialogViewModel viewModel = Container.GetExportedValue<SettingDialogViewModel>();

            Assert.AreEqual(this.soundPath, viewModel.SoundPath);
            Assert.AreEqual("", viewModel.Validate("SoundPath"));

            viewModel.HasAlertSound = true;

            viewModel.SoundPath = "C:\\Music.mp3";
            Assert.AreEqual("C:\\Music.mp3", viewModel.SoundPath);
            Assert.AreEqual("", viewModel.Validate("SoundPath"));

            viewModel.SoundPath = "";
            Assert.AreEqual("", viewModel.SoundPath);
            Assert.AreNotEqual("", viewModel.Validate("SoundPath"));

            viewModel.SoundPath = "1@23";
            Assert.AreEqual("1@23", viewModel.SoundPath);
            Assert.AreNotEqual("", viewModel.Validate("SoundPath"));

            viewModel.HasAlertSound = false;

            viewModel.SoundPath = "";
            Assert.AreEqual("", viewModel.SoundPath);
            Assert.AreEqual("", viewModel.Validate("SoundPath"));

            viewModel.SoundPath = "1@23";
            Assert.AreEqual("1@23", viewModel.SoundPath);
            Assert.AreEqual("", viewModel.Validate("SoundPath"));
        }
    }
}
