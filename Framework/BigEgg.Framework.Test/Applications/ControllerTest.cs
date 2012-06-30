using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using BigEgg.Framework.Applications;
using BigEgg.Framework.Foundation;
using BigEgg.Framework.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigEgg.Framework.Test.Applications
{
    [TestClass]
    public class ControllerTest
    {
        // We have to do every test twice because the Controller and ViewModel class contains the same
        // weak event pattern implementation.
        
        [TestMethod]
        public void AddWeakEventListenerTest() 
        {
            // Add a weak event listener and check if the eventhandler is called
            DocumentManager documentManager = new DocumentManager();
            DocumentController controller = new DocumentController(documentManager);
            Assert.IsFalse(controller.DocumentsHasChanged);
            Assert.IsFalse(controller.ActiveDocumentHasChanged);
            documentManager.Open();
            Assert.IsTrue(controller.DocumentsHasChanged);
            Assert.IsTrue(controller.ActiveDocumentHasChanged);

            // Remove the weak event listener and check that the eventhandler is not anymore called
            controller.RemoveWeakEventListeners();
            controller.DocumentsHasChanged = false;
            controller.ActiveDocumentHasChanged = false;
            documentManager.Open();
            Assert.IsFalse(controller.DocumentsHasChanged);
            Assert.IsFalse(controller.ActiveDocumentHasChanged);

            // Remove again the same weak event listeners although they are not anymore registered
            controller.RemoveWeakEventListeners();

            // Check that the garbage collector is able to collect the controller although the service lives longer
            controller.AddWeakEventListeners();
            documentManager.Open();
            Assert.IsTrue(controller.DocumentsHasChanged);
            Assert.IsTrue(controller.ActiveDocumentHasChanged);
            WeakReference weakController = new WeakReference(controller);
            controller = null;
            GC.Collect();

            Assert.IsFalse(weakController.IsAlive);
        }

        [TestMethod]
        public void AddWeakEventListenerTest2()
        {
            // Add a weak event listener and check if the eventhandler is called
            DocumentManager documentManager = new DocumentManager();
            ShellViewModel viewModel = new ShellViewModel(new MockView(), documentManager);
            Assert.IsFalse(viewModel.DocumentsHasChanged);
            Assert.IsFalse(viewModel.ActiveDocumentHasChanged);
            documentManager.Open();
            Assert.IsTrue(viewModel.DocumentsHasChanged);
            Assert.IsTrue(viewModel.ActiveDocumentHasChanged);

            // Remove the weak event listener and check that the eventhandler is not anymore called
            viewModel.RemoveWeakEventListeners();
            viewModel.DocumentsHasChanged = false;
            viewModel.ActiveDocumentHasChanged = false;
            documentManager.Open();
            Assert.IsFalse(viewModel.DocumentsHasChanged);
            Assert.IsFalse(viewModel.ActiveDocumentHasChanged);

            // Remove again the same weak event listeners although they are not anymore registered
            viewModel.RemoveWeakEventListeners();

            // Check that the garbage collector is able to collect the controller although the service lives longer
            viewModel.AddWeakEventListeners();
            documentManager.Open();
            Assert.IsTrue(viewModel.DocumentsHasChanged);
            Assert.IsTrue(viewModel.ActiveDocumentHasChanged);
            WeakReference weakController = new WeakReference(viewModel);
            viewModel = null;
            GC.Collect();

            Assert.IsFalse(weakController.IsAlive);
        }

        [TestMethod]
        public void ArgumentNullTest()
        {
            DocumentManager documentManager = new DocumentManager();
            DocumentController controller = new DocumentController(documentManager);
            ShellViewModel shellViewModel = new ShellViewModel(new MockView(), documentManager);
            
            AssertHelper.ExpectedException<ArgumentNullException>(() => controller.AddWeakEventListener((INotifyPropertyChanged)null, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => controller.AddWeakEventListener(documentManager, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => controller.RemoveWeakEventListener((INotifyPropertyChanged)null, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => controller.RemoveWeakEventListener(documentManager, null));

            AssertHelper.ExpectedException<ArgumentNullException>(() => controller.AddWeakEventListener((INotifyCollectionChanged)null, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => controller.AddWeakEventListener((INotifyCollectionChanged)documentManager.Documents, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => controller.RemoveWeakEventListener((INotifyCollectionChanged)null, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => controller.RemoveWeakEventListener((INotifyCollectionChanged)documentManager.Documents, null));

            AssertHelper.ExpectedException<ArgumentNullException>(() => shellViewModel.AddWeakEventListener((INotifyPropertyChanged)null, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => shellViewModel.AddWeakEventListener(documentManager, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => shellViewModel.RemoveWeakEventListener((INotifyPropertyChanged)null, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => shellViewModel.RemoveWeakEventListener(documentManager, null));

            AssertHelper.ExpectedException<ArgumentNullException>(() => shellViewModel.AddWeakEventListener((INotifyCollectionChanged)null, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => shellViewModel.AddWeakEventListener((INotifyCollectionChanged)documentManager.Documents, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => shellViewModel.RemoveWeakEventListener((INotifyCollectionChanged)null, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => shellViewModel.RemoveWeakEventListener((INotifyCollectionChanged)documentManager.Documents, null));

            AssertHelper.ExpectedException<ArgumentNullException>(() => new PropertyChangedEventListener(null, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => new PropertyChangedEventListener(documentManager, null));

            AssertHelper.ExpectedException<ArgumentNullException>(() => new CollectionChangedEventListener(null, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => new CollectionChangedEventListener((INotifyCollectionChanged)documentManager.Documents, null));
        }

        [TestMethod]
        public void InitializeTest()
        {
            DocumentManager documentManager = new DocumentManager();
            DocumentController controller = new DocumentController(documentManager);

            Assert.AreEqual(false, controller.IsInitialized);
            controller.Initialize();
            Assert.AreEqual(true, controller.IsInitialized);
        }

        private class DocumentController : Controller
        {
            private readonly IDocumentManager _DocumentManager;


            public DocumentController(IDocumentManager documentManager)
            {
                _DocumentManager = documentManager;
                AddWeakEventListeners();
            }


            public bool DocumentsHasChanged { get; set; }
            public bool ActiveDocumentHasChanged { get; set; }


            protected override void OnInitialize()
            {
                base.OnInitialize();
            }


            public void AddWeakEventListeners()
            {
                AddWeakEventListener(_DocumentManager.Documents, DocumentsCollectionChanged);
                AddWeakEventListener(_DocumentManager, DocumentManagerPropertyChanged);
            }

            public void RemoveWeakEventListeners()
            {
                RemoveWeakEventListener(_DocumentManager.Documents, DocumentsCollectionChanged);
                RemoveWeakEventListener(_DocumentManager, DocumentManagerPropertyChanged);
            }

            public new void AddWeakEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
            {
                base.AddWeakEventListener(source, handler);
            }

            public new void RemoveWeakEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
            {
                base.RemoveWeakEventListener(source, handler);
            }

            public new void AddWeakEventListener(INotifyCollectionChanged source, NotifyCollectionChangedEventHandler handler)
            {
                base.AddWeakEventListener(source, handler);
            }

            public new void RemoveWeakEventListener(INotifyCollectionChanged source, NotifyCollectionChangedEventHandler handler)
            {
                base.RemoveWeakEventListener(source, handler);
            }


            private void DocumentsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                DocumentsHasChanged = true;
            }

            private void DocumentManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "ActiveDocument")
                {
                    ActiveDocumentHasChanged = true;
                }
            }
        }

        private class ShellViewModel : ViewModel
        {
            private readonly IDocumentManager _DocumentManager;


            public ShellViewModel(IView view, IDocumentManager documentManager) : base(view)
            {
                _DocumentManager = documentManager;
                AddWeakEventListeners();
            }


            public bool DocumentsHasChanged { get; set; }
            public bool ActiveDocumentHasChanged { get; set; }

            
            public void AddWeakEventListeners()
            {
                AddWeakEventListener(_DocumentManager.Documents, DocumentsCollectionChanged);
                AddWeakEventListener(_DocumentManager, DocumentManagerPropertyChanged);
            }

            public void RemoveWeakEventListeners()
            {
                RemoveWeakEventListener(_DocumentManager.Documents, DocumentsCollectionChanged);
                RemoveWeakEventListener(_DocumentManager, DocumentManagerPropertyChanged);
            }

            public new void AddWeakEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
            {
                base.AddWeakEventListener(source, handler);
            }

            public new void RemoveWeakEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
            {
                base.RemoveWeakEventListener(source, handler);
            }

            public new void AddWeakEventListener(INotifyCollectionChanged source, NotifyCollectionChangedEventHandler handler)
            {
                base.AddWeakEventListener(source, handler);
            }

            public new void RemoveWeakEventListener(INotifyCollectionChanged source, NotifyCollectionChangedEventHandler handler)
            {
                base.RemoveWeakEventListener(source, handler);
            }

            private void DocumentsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                DocumentsHasChanged = true;
            }

            private void DocumentManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "ActiveDocument")
                {
                    ActiveDocumentHasChanged = true;
                }
            }
        }

        private class MockView : IView
        {
            public object DataContext { get; set; }
        }

        private interface IDocumentManager : INotifyPropertyChanged
        {
            ObservableCollection<object> Documents { get; }
            
            object ActiveDocument { get; }
        }

        private class DocumentManager : Model, IDocumentManager
        {
            private ObservableCollection<object> _Documents = new ObservableCollection<object>();
            private object _ActiveDocument;


            public ObservableCollection<object> Documents { get { return _Documents; } }

            public object ActiveDocument 
            { 
                get { return _ActiveDocument; } 
                private set 
                {
                    if (_ActiveDocument != value) 
                    {
                        _ActiveDocument = value;
                        RaisePropertyChanged("ActiveDocument");
                    }
                }
            }


            public void Open()
            {
                Documents.Add(new object());
                ActiveDocument = Documents.Last();
            }
        }
    }
}
