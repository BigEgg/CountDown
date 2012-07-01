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
using CountDown.Application.Domain;
using CountDown.Application.Models;
using CountDown.Application.Properties;
using CountDown.Application.Services;
using CountDown.Application.ViewModels.Dialog;
using CountDown.Application.Views.Dialog;

namespace CountDown.Application.Controllers
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

        private AlartDialogViewModel alartDialogViewModel;
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
            newItem.AlartTime = newItem.Time.AddMinutes(0 - Settings.Default.DefautBeforeAlartMinutes);

            this.dataService.CountDownItems.Add(newItem);

            if (!this.dataService.Branches.Contains(newModel.NoticeBranch))
            {
                this.dataService.Branches.Add(newModel.NoticeBranch);
            }

            if (Settings.Default.ResetCountDownData)
            {
                newModel.Days = 0;
                newModel.Hours = 0;
                newModel.Minutes = 0;
                newModel.NoticeBranch = string.Empty;
                newModel.Notice = string.Empty;
                newModel.BeforeAlartMinutes = Settings.Default.DefautBeforeAlartMinutes;
            }
        }

        private bool CanDeleteCountDownItemCommand() { return (this.dataService.SelectItems.Count != 0); }

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
                    item.AlartTime.ToBinary(),
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
                    AlartTime = DateTime.Parse(line[1]),
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
            else if (e.PropertyName == "AlartItems")
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
            this.dataService.CheckAlartItems();
        }

        private void ShowAlart()
        {
            // Show the alart dialg view to the user
            IAlartDialogView alartDialog = container.GetExportedValue<IAlartDialogView>();

            if ((this.alartDialogViewModel != null) && (this.alartDialogViewModel.HasShow))
            {
                DateTime lastTime = this.alartDialogViewModel.Items.Max(c => c.AlartTime);
                List<ICountDownItem> newAlartItem = this.dataService.AlartItems.Where(
                    c => c.AlartTime > lastTime).ToList();

                foreach (ICountDownItem item in newAlartItem)
                    this.alartDialogViewModel.Items.Add(item);
            }
            else
            {
                this.alartDialogViewModel = new AlartDialogViewModel(alartDialog, this.dataService.AlartItems);
                this.alartDialogViewModel.ShowDialog(shellService.ShellView);
            }

            if (Settings.Default.HasAlartSound)
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
