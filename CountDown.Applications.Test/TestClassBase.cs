using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using CountDown.Applications.Controllers;
using CountDown.Applications.Services;
using CountDown.Applications.Test.Services;
using CountDown.Applications.Test.Views;
using CountDown.Applications.Test.Views.Dialogs;
using CountDown.Applications.Test.Views.NewItemViews;
using CountDown.Applications.ViewModels;
using CountDown.Applications.ViewModels.Dialogs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountDown.Applications.Test
{
    [TestClass]
    public abstract class TestClassBase
    {
        private readonly CompositionContainer container;


        protected TestClassBase()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new TypeCatalog(
                typeof(ApplicationController), typeof(DataController), typeof(NewItemsController),
                typeof(ShellService), typeof(DataService),
                typeof(ItemListViewModel), typeof(NewItemsViewModel), typeof(ShellViewModel), 
                typeof(AlertDialogViewModel), typeof(AboutDialogViewModel), typeof(SettingDialogViewModel)
            ));
            catalog.Catalogs.Add(new TypeCatalog(
                typeof(MockPresentationService),
                typeof(MockMessageService), typeof(MockFileDialogService),
                typeof(MockShellView), typeof(MockItemListViewModel), typeof(MockNewItemsView),
                typeof(MockAlertAtTimeView), typeof(MockCountDownAlertView),
                typeof(MockAboutDialogView), typeof(MockAlertDialogView), typeof(MockSettingDialogView)
            ));
            container = new CompositionContainer(catalog);
            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(container);
            container.Compose(batch);
        }


        protected CompositionContainer Container { get { return container; } }


        [TestInitialize]
        public void TestInitialize()
        {
            OnTestInitialize();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            OnTestCleanup();
        }

        protected virtual void OnTestInitialize() { }

        protected virtual void OnTestCleanup() { }
    }
}
