using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.Threading;
using BigEgg.Framework.Applications;
using BigEgg.Framework.Applications.Services;
using CountDown.Applications.Properties;
using CountDown.Applications.Services;
using CountDown.Applications.ViewModels;
using CountDown.Applications.ViewModels.Dialogs;
using CountDown.Applications.Views.Dialogs;

namespace CountDown.Applications.Controllers
{
    [Export(typeof(IApplicationController))]
    internal class ApplicationController : Controller, IApplicationController
    {
        private readonly CompositionContainer container;
        private readonly DataController dataController;
        private readonly NewItemsController newItemsController;

        private readonly ShellViewModel shellViewModel;
        private readonly ItemListViewModel itemListViewModel;
        private readonly NewItemsViewModel newItemsViewModel;
        private readonly DelegateCommand exitCommand;
        private readonly DelegateCommand settingCommand;
        private readonly DelegateCommand aboutCommand;

        [ImportingConstructor]
        public ApplicationController(CompositionContainer container, IPresentationService presentationService,
            ShellService shellService, DataController dataController, NewItemsController newItemsController)
        {
            InitializeCultures();
            presentationService.InitializeCultures();

            this.container = container;
            this.dataController = dataController;
            this.newItemsController = newItemsController;
            this.shellViewModel = container.GetExportedValue<ShellViewModel>();
            this.itemListViewModel = container.GetExportedValue<ItemListViewModel>();
            this.newItemsViewModel = container.GetExportedValue<NewItemsViewModel>();

            shellService.ShellView = shellViewModel.View;
            shellService.ItemListView = itemListViewModel.View;
            shellService.NewItemsView = newItemsViewModel.View;
            this.shellViewModel.Closing += ShellViewModelClosing;
            this.exitCommand = new DelegateCommand(Close);
            this.settingCommand = new DelegateCommand(SettingDialogCommand);
            this.aboutCommand = new DelegateCommand(AboutDialogCommand);
        }


        protected override void OnInitialize()
        {
            this.shellViewModel.ExitCommand = this.exitCommand;
            this.shellViewModel.SettingCommand = this.settingCommand;
            this.shellViewModel.AboutCommand = this.aboutCommand;

            this.dataController.Initialize();
            this.newItemsController.Initialize();

            this.newItemsViewModel.ResetCountDownData = Settings.Default.ResetCountDownData;
            this.newItemsViewModel.AlertBeforeMinutes = Settings.Default.DefaultAlertBeforeMinutes;
        }

        public void Run()
        {
            this.shellViewModel.Show();
        }

        public void Shutdown()
        {
            this.dataController.Shutdown();
            this.newItemsController.Shutdown();

            if (this.shellViewModel.NewLanguage != null)
            {
                Settings.Default.UICulture = this.shellViewModel.NewLanguage.Name;
                Settings.Default.Culture = this.shellViewModel.NewLanguage.Name;
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
            ISettingDialogView view = container.GetExportedValue<ISettingDialogView>();
            IDataService dataService = container.GetExportedValue<IDataService>();
            IFileDialogService fileDialogService = container.GetExportedValue<IFileDialogService>();
            SettingDialogViewModel settingDialog = new SettingDialogViewModel(view, dataService, fileDialogService);

            settingDialog.BeforeAlertMinutes = Settings.Default.DefaultAlertBeforeMinutes;
            settingDialog.ExpiredMinutes = Settings.Default.DefaultExpiredMinutes;
            settingDialog.HasAlertSound = Settings.Default.HasAlertSound;
            settingDialog.SoundPath = Settings.Default.SoundPath;
            settingDialog.ResetCountDownData = Settings.Default.ResetCountDownData;

            bool? result = settingDialog.ShowDialog(this.shellViewModel.View);

            if (result == true)
            {
                Settings.Default.DefaultAlertBeforeMinutes = settingDialog.BeforeAlertMinutes;
                Settings.Default.DefaultExpiredMinutes = settingDialog.ExpiredMinutes;
                Settings.Default.HasAlertSound = settingDialog.HasAlertSound;
                Settings.Default.SoundPath = settingDialog.SoundPath;
                Settings.Default.ResetCountDownData = settingDialog.ResetCountDownData;
                Settings.Default.Save();

                this.newItemsViewModel.ResetCountDownData = Settings.Default.ResetCountDownData;
                this.newItemsViewModel.AlertBeforeMinutes = Settings.Default.DefaultAlertBeforeMinutes;
            }
        }

        private void AboutDialogCommand()
        {
            IAboutDialogView view = container.GetExportedValue<IAboutDialogView>();
            AboutDialogViewModel aboutDialog = new AboutDialogViewModel(view);
            aboutDialog.ShowDialog(this.shellViewModel.View);
        }
    }
}
