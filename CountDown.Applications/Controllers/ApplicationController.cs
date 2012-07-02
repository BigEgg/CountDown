using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.Threading;
using BigEgg.Framework.Applications;
using CountDown.Applications.Properties;
using CountDown.Applications.Services;
using CountDown.Applications.ViewModels;
using CountDown.Applications.ViewModels.Dialog;

namespace CountDown.Applications.Controllers
{
    [Export(typeof(IApplicationController))]
    internal class ApplicationController : Controller, IApplicationController
    {
        private readonly CompositionContainer container;
        private readonly DataController dataController;
        private readonly ShellViewModel shellViewModel;
        private readonly MainViewModel mainViewModel;
        private readonly SettingDialogViewModel settingDialog;
        private readonly DelegateCommand exitCommand;
        private readonly DelegateCommand settingCommand;

        [ImportingConstructor]
        public ApplicationController(CompositionContainer container, IPresentationService presentationService, 
            ShellService shellService, DataController dataController)
        {
            InitializeCultures();
            presentationService.InitializeCultures();

            this.container = container;
            this.dataController = dataController;
            this.shellViewModel = container.GetExportedValue<ShellViewModel>();
            this.mainViewModel = container.GetExportedValue<MainViewModel>();
            this.settingDialog = container.GetExportedValue<SettingDialogViewModel>();

            shellService.ShellView = shellViewModel.View;
            this.shellViewModel.Closing += ShellViewModelClosing;
            this.exitCommand = new DelegateCommand(Close);
            this.settingCommand = new DelegateCommand(SettingDialogCommand);
        }


        protected override void OnInitialize()
        {
            this.mainViewModel.ExitCommand = this.exitCommand;

            this.dataController.Initialize();
        }

        public void Run()
        {
            this.shellViewModel.ContentView = mainViewModel.View;

            this.shellViewModel.Show();
        }

        public void Shutdown()
        {
            this.dataController.Shutdown();

            if (this.mainViewModel.NewLanguage != null)
            {
                Settings.Default.UICulture = this.mainViewModel.NewLanguage.Name;
            }
            try
            {
                Settings.Default.Save();
            }
            catch (Exception)
            {
                // When more application instances are closed at the same time then an exception occurs.
            }
        }


        private static void InitializeCultures()
        {
            if (!String.IsNullOrEmpty(Settings.Default.Culture))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Settings.Default.Culture);
            }
            if (!String.IsNullOrEmpty(Settings.Default.UICulture))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.UICulture);
            }
        }

        private void Close()
        {
            shellViewModel.Close();
        }

        private void ShellViewModelClosing(object sender, CancelEventArgs e)
        {
        }

        private void SettingDialogCommand()
        {
            this.settingDialog.BeforeAlertMinutes = Settings.Default.DefautBeforeAlertMinutes;
            this.settingDialog.ExpiredMinutes = Settings.Default.DefaultExpiredMinutes;
            this.settingDialog.HasAlertSound = Settings.Default.HasAlertSound;
            this.settingDialog.SoundPath = Settings.Default.SoundPath;
            this.settingDialog.ResetCountDownData = Settings.Default.ResetCountDownData;

            bool? result = settingDialog.ShowDialog(this.shellViewModel.View);

            if (result == true)
            {
                Settings.Default.DefautBeforeAlertMinutes = this.settingDialog.BeforeAlertMinutes;
                Settings.Default.DefaultExpiredMinutes = this.settingDialog.ExpiredMinutes;
                Settings.Default.HasAlertSound = this.settingDialog.HasAlertSound;
                Settings.Default.SoundPath = this.settingDialog.SoundPath;
                Settings.Default.ResetCountDownData = this.settingDialog.ResetCountDownData;
                Settings.Default.Save();

                this.mainViewModel.HasAlertSound = Settings.Default.HasAlertSound;
                this.mainViewModel.ResetCountDownData = Settings.Default.ResetCountDownData;
            }
        }
    }
}
