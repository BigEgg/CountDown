using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BigEgg.Framework;
using BigEgg.Framework.Foundation;
using BigEgg.Framework.UnitTesting;
using System.ComponentModel;

namespace BigEgg.Framework.Test.Foundation
{
    [TestClass]
    public class ModelTest
    {
        private bool originalDebugSetting;


        public TestContext TestContext { get; set; }


        [TestInitialize]
        public void TestInitialize()
        {
            this.originalDebugSetting = BEConfiguration.Debug;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            BEConfiguration.Debug = this.originalDebugSetting;
        }

        [TestMethod]
        public void RaisePropertyChangedTest()
        {
            Person luke = new Person();

            BEConfiguration.Debug = true;
            AssertHelper.PropertyChangedEvent(luke, x => x.Name, () => luke.Name = "Luke");

            BEConfiguration.Debug = false;
            AssertHelper.PropertyChangedEvent(luke, x => x.Name, () => luke.Name = "Skywalker");
        }

        [TestMethod]
        public void AddAndRemoveEventHandler()
        {
            Person luke = new Person();
            bool eventRaised;

            PropertyChangedEventHandler eventHandler = (sender, e) =>
            {
                eventRaised = true;
            };

            eventRaised = false;
            luke.PropertyChanged += eventHandler;
            luke.Name = "Luke";
            Assert.IsTrue(eventRaised, "The property changed event needs to be raised");

            eventRaised = false;
            luke.PropertyChanged -= eventHandler;
            luke.Name = "Luke Skywalker";
            Assert.IsFalse(eventRaised, "The event handler must not be called because it was removed from the event.");
        }

        [TestMethod]
        public void WrongPropertyName()
        {
            WrongDocument document = new WrongDocument();

            BEConfiguration.Debug = true;
            AssertHelper.ExpectedException<InvalidOperationException>(() =>
                document.Header = "BigEgg Application Framework");

            BEConfiguration.Debug = false;
            document.Header = "BigEgg Application Framework";
        }

        [Serializable]
        private class Person : Model
        {
            private string _Name;

            public string Name
            {
                get { return _Name; }
                set
                {
                    if (_Name != value)
                    {
                        _Name = value;
                        RaisePropertyChanged("Name");
                    }
                }
            }
        }

        private class WrongDocument : Model
        {
            private string _Header;

            public string Header
            {
                get { return _Header; }
                set
                {
                    if (_Header != value)
                    {
                        _Header = value;
                        RaisePropertyChanged("WrongPropertyName");
                    }
                }
            }
        }
    }
}
