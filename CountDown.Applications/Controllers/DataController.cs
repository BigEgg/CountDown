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
using System.Windows.Media;
using BigEgg.Framework.Applications;
using BigEgg.Framework.Applications.Services;
using BigEgg.Framework.Foundation;
using CountDown.Applications.Domain;
using CountDown.Applications.Models;
using CountDown.Applications.Properties;
using CountDown.Applications.Services;
using CountDown.Applications.ViewModels.Dialog;
using CountDown.Applications.Views.Dialog;

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
        private readonly DelegateCommand newCountDownItemCommand;
        private readonly DelegateCommand deleteCountDownItemCommand;

        private Timer cleanExpiredTimer = null;

        private AlertDialogViewModel AlertDialogViewModel;
        private MediaPlayer player = null;
        #endregion

        [ImportingConstructor]
        public DataController(CompositionContainer container, IMessageService messageService, 
            IShellService shellService, DataService dataService)
        {
            this.container = container;
            this.messageService = messageService;
            this.shellService = shellService;
            this.dataService = dataService;

            this.newCountDownItemCommand = new DelegateCommand(NewCountDownItemCommand, CanNewCountDownItemCommand);
            this.deleteCountDownItemCommand = new DelegateCommand(DeleteCountDownItemCommand, CanDeleteCountDownItemCommand);

            this.dataService.NewCountDownItem = this.newCountDownItemCommand;
            this.dataService.DeleteCountDownItem = this.deleteCountDownItemCommand;

            AddWeakEventListener(this.dataService, DataServicePropertyChanged);
        }

        #region Methods
        protected override void OnInitialize()
        {
            LoadData();

            int dueTime;
            DateTime startTime = DateTime.Now;
            dueTime = 60000 - startTime.Second * 1000 - startTime.Millisecond + 10;

            // Create an inferred delegate that invokes methods for the timer.
            TimerCallback tcb = TimerCallbackMethods;

            cleanExpiredTimer = new Timer(tcb, null, dueTime, 60000);
        }

        public void Shutdown()
        {
            try
            {
                SaveData();

                cleanExpiredTimer.Dispose();
            }
            catch (Exception ex)
            {
                messageService.ShowError(shellService.ShellView,
                    string.Format(CultureInfo.CurrentCulture, Resources.SaveError, ex.Message));
            }
        }

        #region Command Methods
        private bool CanNewCountDownItemCommand()
        {
            NewCountDownModel newModel = this.dataService.NewCountDownModel;
            if (string.IsNullOrEmpty(newModel.Validate()))
                return true;
            else
                return false;
        }

        private void NewCountDownItemCommand() 
        {
            NewCountDownModel newModel = this.dataService.NewCountDownModel;
            ICountDownItem newItem = new CountDownItem();
            newItem.Time = DateTime.Now;

            newItem.Time.Add(new TimeSpan(newModel.Days, newModel.Hours, newModel.Minutes, 0));
            newItem.Notice = string.Format(Resources.NoticeFormat, newModel.NoticeBranch, newModel.Notice);
            newItem.AlertTime = newItem.Time.AddMinutes(0 - Settings.Default.DefautBeforeAlertMinutes);

            this.dataService.CountDownItems.Add(newItem);

            if (!this.dataService.Branches.Contains(newModel.NoticeBranch))
            {
                this.dataService.Branches.Add(newModel.NoticeBranch);
            }
            else
            {
                int index  = this.dataService.Branches.IndexOf(newModel.NoticeBranch);
                this.dataService.Branches.Move(index, 0);
            }

            if (Settings.Default.ResetCountDownData)
            {
                newModel.Days = 0;
                newModel.Hours = 0;
                newModel.Minutes = 0;
                newModel.NoticeBranch = string.Empty;
                newModel.Notice = string.Empty;
                newModel.BeforeAlertMinutes = Settings.Default.DefautBeforeAlertMinutes;
            }
        }

        private bool CanDeleteCountDownItemCommand() { return (this.dataService.SelectItems.Any()); }

        private void DeleteCountDownItemCommand()
        {
            foreach (ICountDownItem item in this.dataService.SelectItems)
            {
                this.dataService.CountDownItems.Remove(item);
            }
        }
        #endregion

        #region Private Mehtods
        private void SaveData()
        {
            StreamWriter sw = new StreamWriter(DataFileName, false);
            sw.WriteLine("CountDown Application (V 1.0)");
            sw.WriteLine("Build for my mom. O(∩_∩)O~");

            foreach (ICountDownItem item in this.dataService.CountDownItems)
            {
                sw.WriteLine(string.Format(
                    "{0}|{1}|{2}",
                    item.Time.ToString(),
                    item.AlertTime.ToBinary(),
                    item.Notice)
                );
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
            StreamReader sr = new StreamReader(DataFileName);
            sr.ReadLine();
            sr.ReadLine();

            this.dataService.CountDownItems.Clear();
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine().Split('|');
                ICountDownItem item = new CountDownItem
                {
                    Time = DateTime.Parse(line[0]),
                    AlertTime = DateTime.Parse(line[1]),
                    Notice = line[2]
                };
                this.dataService.CountDownItems.Add(item);
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
            if ((e.PropertyName == "SelectItems") && (e.PropertyName == "NewCountDownModel"))
            {
                UpdateCommands();
            }
            else if (e.PropertyName == "AlertItems")
            {
                
            }

        }

        private void UpdateCommands()
        {
            this.newCountDownItemCommand.RaiseCanExecuteChanged();
            this.deleteCountDownItemCommand.RaiseCanExecuteChanged();
        }

        // This method is called by the timer delegate.
        private void TimerCallbackMethods(Object obj)
        {
            this.dataService.CleanExpiredItems();
            this.dataService.CheckAlertItems();
        }

        private void ShowAlert()
        {
            // Show the Alert dialg view to the user
            IAlertDialogView AlertDialog = container.GetExportedValue<IAlertDialogView>();

            if ((this.AlertDialogViewModel != null) && (this.AlertDialogViewModel.HasShow))
            {
                DateTime lastTime = this.AlertDialogViewModel.Items.Max(c => c.AlertTime);
                List<ICountDownItem> newAlertItem = this.dataService.AlertItems.Where(
                    c => c.AlertTime > lastTime).ToList();

                foreach (ICountDownItem item in newAlertItem)
                    this.AlertDialogViewModel.Items.Add(item);
            }
            else
            {
                this.AlertDialogViewModel = new AlertDialogViewModel(AlertDialog, this.dataService.AlertItems);
                this.AlertDialogViewModel.ShowDialog(shellService.ShellView);
            }

            if (Settings.Default.HasAlertSound)
            {
                if (File.Exists(Settings.Default.SoundPath))
                {
                    if (this.player == null) { this.player = new MediaPlayer(); }
                    player.Stop();
                    player.Open(new Uri(Settings.Default.SoundPath, UriKind.Relative));
                    player.Play();
                }
            }
        }
        #endregion
        #endregion
    }
}
