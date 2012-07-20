using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using BigEgg.Framework.Applications;
using BigEgg.Framework.Applications.Services;
using CountDown.Applications.Domain;
using CountDown.Applications.Properties;
using CountDown.Applications.Services;
using CountDown.Applications.ViewModels.Dialogs;
using CountDown.Applications.Views.Dialogs;
using CountDown.Applications.ViewModels;

namespace CountDown.Applications.Controllers
{
    [Export]
    internal class DataController : Controller
    {
        #region Members
        private const string DataFileName = "CountDown.data";

        private readonly CompositionContainer container;
        private readonly IMessageService messageService;
        private readonly IShellService shellService;
        private readonly DataService dataService;
        private ShellViewModel shellViewModel;
        private AlertDialogViewModel alertDialogViewModel;

        private Timer cleanExpiredTimer = null;
        #endregion

        [ImportingConstructor]
        public DataController(CompositionContainer container, IMessageService messageService, 
            IShellService shellService, DataService dataService)
        {
            this.container = container;
            this.messageService = messageService;
            this.shellService = shellService;
            this.dataService = dataService;

            AddWeakEventListener(this.dataService, DataServicePropertyChanged);
        }

        #region Methods
        protected override void OnInitialize()
        {
            this.shellViewModel = container.GetExportedValue<ShellViewModel>();

            LoadData();

            this.dataService.SelectedItem = this.dataService.Items.FirstOrDefault();

            int dueTime;
            DateTime startTime = DateTime.Now;
            dueTime = 60000 - startTime.Second * 1000 - startTime.Millisecond + 10;

            // Create an inferred delegate that invokes methods for the timer.
            TimerCallback tcb = TimerCallbackMethods;

            this.cleanExpiredTimer = new Timer(tcb, null, dueTime, 60000);
        }

        public void Shutdown()
        {
            try
            {
                SaveData();

                if (this.cleanExpiredTimer != null)
                {
                    this.cleanExpiredTimer.Change(0, Timeout.Infinite);
                    this.cleanExpiredTimer.Dispose();
                    this.cleanExpiredTimer = null;
                }
            }
            catch (Exception ex)
            {
                messageService.ShowError(shellService.ShellView,
                    string.Format(CultureInfo.CurrentCulture, Resources.SaveError, ex.Message));
            }
        }

        #region Private Mehtods
        private void SaveData()
        {
            using (StreamWriter sw = new StreamWriter(DataFileName, false))
            {
                sw.WriteLine("CountDown Application (V 1.0)");
                sw.WriteLine("Build for my mom. O(∩_∩)O~");

                foreach (IAlertItem item in this.dataService.Items)
                {
                    sw.WriteLine(string.Format(
                        "{0}|{1}|{2}",
                        item.Time.ToString(),
                        item.AlertTime.ToString(),
                        item.Notice)
                    );
                }
            }

            StringCollection branches = new StringCollection();
            branches.AddRange(this.dataService.Branches.ToArray());
            Settings.Default.Branches = branches;
        }

        private void LoadData()
        {
            if (!File.Exists(DataFileName))
            {
                return;
            }

            string [] line;
            using (StreamReader sr = new StreamReader(DataFileName))
            {
                sr.ReadLine();
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Split('|');
                    IAlertItem item = new AlertItem
                    {
                        Time = DateTime.Parse(line[0]),
                        AlertTime = DateTime.Parse(line[1]),
                        Notice = line[2]
                    };
                    this.dataService.Items.Add(item);
                }
            }

            if (Settings.Default.Branches != null)
            {
                for (int i = 0; i < Settings.Default.Branches.Count; i++)
                {
                    this.dataService.Branches.Add(Settings.Default.Branches[i]);
                }
            }
        }

        private void DataServicePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AlertedItems")
            {
                ShowAlert();
            }
            else if (e.PropertyName == "Items")
            {
                this.shellViewModel.ItemCount = this.dataService.Items.Count;
            }
        }

        private void ShowAlert()
        {
            // Show the Alert dialg view to the user
            IAlertDialogView alertDialog = container.GetExportedValue<IAlertDialogView>();
            List<IAlertItem> newAlertItem = this.dataService.AlertedItems.Where(c => !c.HasAlert).ToList(); ;

            if (this.alertDialogViewModel == null)
            {
                MultiThreadingObservableCollection<IAlertItem> items = new MultiThreadingObservableCollection<IAlertItem>();

                this.alertDialogViewModel = new AlertDialogViewModel(alertDialog, items);
            }
            foreach (IAlertItem item in newAlertItem)
            {
                if (!this.alertDialogViewModel.Items.Contains(item))
                    this.alertDialogViewModel.Items.Add(item);
            }
            this.alertDialogViewModel.HasAlertSound = Settings.Default.HasAlertSound;

            this.alertDialogViewModel.ShowDialog(this.shellService.ShellView);
        }

        // This method is called by the timer delegate.
        private void TimerCallbackMethods(Object obj)
        {
            try
            {
                CleanExpiredItems();
                CheckAlertItems();
            }
            catch
            {
                throw;
            }
        }

        private void CleanExpiredItems()
        {
            DateTime expiredTime = DateTime.Now.AddMinutes(0 - Settings.Default.DefaultExpiredMinutes);
            List<IAlertItem> expiredItems = this.dataService.AlertedItems.Where(
                c => (c.Time <= expiredTime) && (c.HasAlert)).ToList();

            foreach (IAlertItem item in expiredItems)
            {
                this.dataService.AlertedItems.Remove(item);
            }
        }

        private void CheckAlertItems()
        {
            List<IAlertItem> newAlertItems = this.dataService.Items.Where(
                i => i.AlertTime <= DateTime.Now).ToList();

            foreach (IAlertItem item in newAlertItems)
            {
                this.dataService.Items.Remove(item);
                this.dataService.AlertedItems.Add(item);
            }
        }
        #endregion
        #endregion
    }
}
