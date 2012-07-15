using BigEgg.Framework.Applications.Services;
using BigEgg.Framework.Foundation;
using CountDown.Applications.Properties;
using CountDown.Applications.Services;
using CountDown.Applications.ViewModels.Dialogs;
using CountDown.Applications.Views.Dialogs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountDown.Applications.Test.ViewModels.Dialogs
{
    [TestClass]
    public class SettingDialogViewModelTest : TestClassBase
    {
        private int beforeAlertMinutes;
        private int expiredMinutes;
        private bool hasAlertSound;
        private string soundPath;
        private bool resetCountDownData;

        SettingDialogViewModel viewModel;

        protected override void OnTestInitialize()
        {
            this.beforeAlertMinutes = Settings.Default.DefaultAlertBeforeMinutes;
            this.expiredMinutes = Settings.Default.DefaultExpiredMinutes;
            this.hasAlertSound = Settings.Default.HasAlertSound;
            this.soundPath = Settings.Default.SoundPath;
            this.resetCountDownData = Settings.Default.ResetCountDownData;

            ISettingDialogView view = Container.GetExportedValue<ISettingDialogView>();
            IDataService dataService = Container.GetExportedValue<IDataService>();
            IFileDialogService fileDialogService = Container.GetExportedValue<IFileDialogService>();
            viewModel = new SettingDialogViewModel(view, dataService, fileDialogService);

            viewModel.BeforeAlertMinutes = this.beforeAlertMinutes;
            viewModel.ExpiredMinutes = this.expiredMinutes;
            viewModel.HasAlertSound = this.hasAlertSound;
            viewModel.SoundPath = this.soundPath;
            viewModel.ResetCountDownData = this.resetCountDownData;
        }

        protected override void OnTestCleanup()
        {
        }

        [TestMethod]
        public void SettingDialogViewModelCloseTest()
        {
            Assert.AreEqual(this.beforeAlertMinutes, viewModel.BeforeAlertMinutes);
            Assert.AreEqual(this.expiredMinutes, viewModel.ExpiredMinutes);
            Assert.AreEqual(this.hasAlertSound, viewModel.HasAlertSound);
            Assert.AreEqual(this.soundPath, viewModel.SoundPath);
            Assert.AreEqual(this.resetCountDownData, viewModel.ResetCountDownData);

            viewModel.BeforeAlertMinutes = 0;
            Assert.AreEqual(false, viewModel.SubmitCommand.CanExecute(null));

            viewModel.BeforeAlertMinutes = this.beforeAlertMinutes + 1;
            viewModel.ExpiredMinutes += 1;
            viewModel.HasAlertSound = !viewModel.HasAlertSound;
            viewModel.SoundPath = "C:\\Music.mp3";
            viewModel.ResetCountDownData = !viewModel.ResetCountDownData;

            Assert.AreEqual(true, viewModel.SubmitCommand.CanExecute(null));
            viewModel.SubmitCommand.Execute(null);

            Assert.AreEqual(this.beforeAlertMinutes + 1, viewModel.BeforeAlertMinutes);
            Assert.AreEqual(this.expiredMinutes + 1, viewModel.ExpiredMinutes);
            Assert.AreEqual(!this.hasAlertSound, viewModel.HasAlertSound);
            Assert.AreEqual("C:\\Music.mp3", viewModel.SoundPath);
            Assert.AreEqual(!this.resetCountDownData, viewModel.ResetCountDownData);
        }

        [TestMethod]
        public void SettingDialogViewModelBeforeAlertMinutesValidationTest()
        {
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

            viewModel.HasAlertSound = true;
            Assert.AreEqual(true, viewModel.HasAlertSound);
            Assert.AreEqual("C:\\Music.mp3", viewModel.SoundPath);
        }

        [TestMethod]
        public void SettingDialogViewModelSoundPathValidationTest()
        {
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
            Assert.AreNotEqual("", viewModel.Validate("SoundPath"));
        }

        [TestMethod]
        public void SettingDialogViewModelAddNewBranchCommandsTest()
        {
            IDataService dataService = Container.GetExportedValue<IDataService>();

            dataService.Branches.Add("Test1");
            dataService.Branches.Add("Test2");
            dataService.Branches.Add("Test3");

            Assert.AreEqual(3, viewModel.Branches.Count);
            Assert.AreEqual(1, viewModel.SelectedBranches.Count);
            Assert.AreEqual("Test1", viewModel.SelectedBranches[0]);

            Assert.AreEqual("", viewModel.NewBranch);
            Assert.AreEqual(true, viewModel.AddNewBranchCommand.CanExecute(null));

            viewModel.NewBranch = "Test";
            Assert.AreEqual(true, viewModel.AddNewBranchCommand.CanExecute(null));

            viewModel.AddNewBranchCommand.Execute(null);
            Assert.AreEqual("", viewModel.NewBranch);
            Assert.AreEqual(4, viewModel.Branches.Count);
            Assert.AreEqual(1, viewModel.SelectedBranches.Count);
            Assert.AreEqual("Test", viewModel.SelectedBranches[0]);

            Assert.AreEqual(4, dataService.Branches.Count);
        }

        [TestMethod]
        public void SettingDialogViewModelRemoveBranchCommandsTest()
        {
            IDataService dataService = Container.GetExportedValue<IDataService>();

            Assert.AreEqual(0, viewModel.SelectedBranches.Count);
            Assert.AreEqual(false, viewModel.RemoveBranchCommand.CanExecute(null));

            dataService.Branches.Add("Test1");
            dataService.Branches.Add("Test2");
            dataService.Branches.Add("Test3");
            dataService.Branches.Add("Test4");
            dataService.Branches.Add("Test5");
            dataService.Branches.Add("Test6");

            Assert.AreEqual(true, viewModel.RemoveBranchCommand.CanExecute(null));

            viewModel.RemoveBranchCommand.Execute(null);
            Assert.AreEqual(5, viewModel.Branches.Count);
            Assert.AreEqual(1, viewModel.SelectedBranches.Count);
            Assert.AreEqual("Test2", viewModel.SelectedBranches[0]);

            viewModel.SelectedBranches.Add(viewModel.Branches[2]);
            viewModel.SelectedBranches.Add(viewModel.Branches[3]);
            viewModel.RemoveBranchCommand.Execute(null);
            Assert.AreEqual(2, viewModel.Branches.Count);
            Assert.AreEqual(1, viewModel.SelectedBranches.Count);
            Assert.AreEqual("Test3", viewModel.SelectedBranches[0]);

            Assert.AreEqual(2, dataService.Branches.Count);
        }
    }
}
