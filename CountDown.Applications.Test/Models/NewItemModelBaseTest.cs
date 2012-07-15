using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CountDown.Applications.Models;
using BigEgg.Framework.Foundation;
using BigEgg.Framework.UnitTesting;

namespace CountDown.Applications.Test.Models
{
    [TestClass]
    public class NewItemModelBaseTest
    {
        [TestMethod]
        public void GeneralNewItemModelTest()
        {
            MockNewItemModel item = new MockNewItemModel();
            item.Time = DateTime.Now;
            item.NoticeBranch = "Test Alert";
            item.Notice = "1";

            Assert.AreEqual("", item.Validate());
        }

        [TestMethod]
        public void PropertiesWithNotification()
        {
            DateTime now = DateTime.Now;

            MockNewItemModel item = new MockNewItemModel();
            Assert.AreEqual(string.Empty, item.NoticeBranch);
            Assert.AreEqual(string.Empty, item.Notice);
            Assert.AreEqual(now, item.Time);

            now = now.AddDays(1);
            AssertHelper.PropertyChangedEvent(item, x => x.Time, () => item.Time = now);
            Assert.AreEqual(now, item.Time);

            AssertHelper.PropertyChangedEvent(item, x => x.NoticeBranch, () => item.NoticeBranch = "Test Alert");
            Assert.AreEqual("Test Alert", item.NoticeBranch);

            AssertHelper.PropertyChangedEvent(item, x => x.Notice, () => item.Notice = "1");
            Assert.AreEqual("1", item.Notice);
        }

        [TestMethod]
        public void NewItemModelTimeValidationTest()
        {
            MockNewItemModel item = new MockNewItemModel();

            Assert.IsNotNull(item.Time);
            Assert.AreEqual("", item.Validate("Time"));

            item.Time = DateTime.Now;
            Assert.AreEqual("", item.Validate("Time"));
        }

        [TestMethod]
        public void NewItemModelNoticeBranchValidationTest()
        {
            MockNewItemModel item = new MockNewItemModel();

            Assert.AreEqual(string.Empty, item.NoticeBranch);
            Assert.AreNotEqual("", item.Validate("NoticeBranch"));

            item.NoticeBranch = "Test Alert";
            Assert.AreEqual("", item.Validate("NoticeBranch"));
        }
    }


    public class MockNewItemModel : NewItemModelBase
    {
    }
}
