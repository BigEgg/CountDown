using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Threading;
using BigEgg.Framework.Applications;
using CountDown.Applications.Properties;
using CountDown.Applications.Services;
using CountDown.Applications.Views;

namespace CountDown.Applications.ViewModels
{
    [Export]
    public class ShellViewModel : ViewModel<IShellView>
    {
        private readonly IShellService shellService;
        private readonly IDataService dataService;
        private object contentView;

        private Timer cleanExpiredTimer = null;

        [ImportingConstructor]
        public ShellViewModel(IShellView view, IDataService dataService,
            IPresentationService presentationService, IShellService shellService)
            : base(view)
        {
            this.shellService = shellService;
            this.dataService = dataService;
            view.Closing += ViewClosing;
            view.Closed += ViewClosed;

            // Restore the window size when the values are valid.
            if (Settings.Default.Left >= 0 && Settings.Default.Top >= 0 && Settings.Default.Width > 0 && Settings.Default.Height > 0
                && Settings.Default.Left + Settings.Default.Width <= presentationService.VirtualScreenWidth
                && Settings.Default.Top + Settings.Default.Height <= presentationService.VirtualScreenHeight)
            {
                ViewCore.Left = Settings.Default.Left;
                ViewCore.Top = Settings.Default.Top;
                ViewCore.Height = Settings.Default.Height;
                ViewCore.Width = Settings.Default.Width;
            }
            ViewCore.IsMaximized = Settings.Default.IsMaximized;
        }


        public string Title { get { return Resources.ApplicationName; } }

        public IShellService ShellService { get { return this.shellService; } }

        public object ContentView
        {
            get { return contentView; }
            set
            {
                if (contentView != value)
                {
                    contentView = value;
                    RaisePropertyChanged("ContentView");
                }
            }
        }


        public event CancelEventHandler Closing;


        public void Show()
        {
            ViewCore.Show();

            int dueTime;
            DateTime startTime = DateTime.Now;
            dueTime = 60000 - startTime.Second * 1000 - startTime.Millisecond + 10;

            // Create an inferred delegate that invokes methods for the timer.
            TimerCallback tcb = TimerCallbackMethods;

            this.cleanExpiredTimer = new Timer(tcb, null, dueTime, 60000);
        }

        public void Close()
        {
            ViewCore.Close();
        }

        protected virtual void OnClosing(CancelEventArgs e)
        {
            if (Closing != null) { Closing(this, e); }
        }

        private void ViewClosing(object sender, CancelEventArgs e)
        {
            OnClosing(e);
        }

        private void ViewClosed(object sender, EventArgs e)
        {
            if (this.cleanExpiredTimer != null)
            {
                this.cleanExpiredTimer.Change(0, Timeout.Infinite);
                this.cleanExpiredTimer.Dispose();
                this.cleanExpiredTimer = null;
            }

            Settings.Default.Left = ViewCore.Left;
            Settings.Default.Top = ViewCore.Top;
            Settings.Default.Height = ViewCore.Height;
            Settings.Default.Width = ViewCore.Width;
            Settings.Default.IsMaximized = ViewCore.IsMaximized;
        }

        // This method is called by the timer delegate.
        private void TimerCallbackMethods(Object obj)
        {
            this.dataService.CleanExpiredItems();
            this.dataService.CheckAlertItems();
        }

    }
}
