using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.Threading;
using BigEgg.Framework.Applications;
using CountDown.Application.Properties;
using CountDown.Application.Services;
using CountDown.Application.ViewModels;
using CountDown.Application.ViewModels.Dialog;

namespace CountDown.Application.Controllers
{
    [Export(typeof(IApplicationController))]
    internal class ApplicationController : Controller, IApplicationController
    {
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

            this.dataController = dataController;

            this.shellViewModel = container.GetExportedValue<ShellViewModel>();
            this.mainViewModel = container.GetExportedValue<MainViewModel>();
            this.settingDialog = container.GetExportedValue<SettingDialogViewModel>();

            shellService.ShellView = shellViewModel.View;
            this.shellViewModel.Closing += ShellViewModelClosing;
            this.exitCommand = new DelegateCommand(Close);
            this.settingCommand = new DelegateCommand(SettingCommand);
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

        private void SettingCommand()
        {
            bool? result = settingDialog.ShowDialog(this.shellViewModel.View);

            if (result == true)
            {
                this.mainViewModel.HasAlertSound = Settings.Default.HasAlertSound;
                this.mainViewModel.ResetCountDownData = Settings.Default.ResetCountDownData;
            }
        }
    }
}
