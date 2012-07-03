using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using BigEgg.Framework;
using BigEgg.Framework.Applications;
using CountDown.Applications.Controllers;
using CountDown.Applications.ViewModels;

namespace CountDown.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private CompositionContainer container;
        private IApplicationController applicationController;


        static App()
        {
#if (DEBUG)
            BEConfiguration.Debug = true;
#endif
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if (DEBUG != true)
            // Don't handle the exceptions in Debug mode because otherwise the Debugger wouldn't
            // jump into the code when an exception occurs.
            DispatcherUnhandledException += AppDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
#endif

            AggregateCatalog catalog = new AggregateCatalog();
            // Add the WpfApplicationFramework assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Controller).Assembly));
            // Add the Waf.BookLibrary.Library.Presentation assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            // Add the Waf.BookLibrary.Library.Applications assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ShellViewModel).Assembly));

            this.container = new CompositionContainer(catalog);
            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(container);
            this.container.Compose(batch);

            this.applicationController = container.GetExportedValue<IApplicationController>();
            this.applicationController.Initialize();
            this.applicationController.Run();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.applicationController.Shutdown();
            this.container.Dispose();

            base.OnExit(e);
        }

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception, false);
            e.Handled = true;
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception, e.IsTerminating);
        }

        private static void HandleException(Exception e, bool isTerminating)
        {
            if (e == null) { return; }

            Trace.TraceError(e.ToString());

            if (!isTerminating)
            {
                MessageBox.Show(string.Format(CultureInfo.CurrentCulture,
                        CountDown.Presentation.Properties.Resources.UnknownError, e.ToString())
                    , ApplicationInfo.ProductName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
