using System;
using BigEgg.Framework.Foundation;
using BigEgg.Framework.UnitTesting;
using CountDown.Applications.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountDown.Applications.Test.Domain
{
    [TestClass]
    public class AlertItemTest
    {
        [TestMethod]
        public void GeneralAlertItemTest()
        {
            AlertItem item = new AlertItem();
            item.Time = DateTime.Now;
            item.AlertTime = DateTime.Now.AddHours(1);
            item.Notice = "Test Alert";
            item.HasAlert = false;

            Assert.AreEqual("", item.Validate());
        }

        [TestMethod]
        public void PropertiesWithNotification()
        {
            DateTime now = DateTime.Now;

            AlertItem item = new AlertItem();
            Assert.AreEqual(false, item.HasAlert);

            AssertHelper.PropertyChangedEvent(item, x => x.Time, () => item.Time = now);
            Assert.AreEqual(now, item.Time);

            AssertHelper.PropertyChangedEvent(item, x => x.AlertTime, () => item.AlertTime = now.AddHours(1));
            Assert.AreEqual(now.AddHours(1), item.AlertTime);
           
            AssertHelper.PropertyChangedEvent(item, x => x.Notice, () => item.Notice = "Test Alert");
            Assert.AreEqual("Test Alert", item.Notice);

            item.HasAlert = true;
            Assert.AreEqual(true, item.HasAlert);
        }

        [TestMethod]
        public void AlertItemTimeValidationTest()
        {
            AlertItem item = new AlertItem();

            Assert.IsNotNull(item.Time);
            Assert.AreEqual("", item.Validate("Time"));

            item.Time = DateTime.Now;
            Assert.AreEqual("", item.Validate("Time"));
        }

        [TestMethod]
        public void AlertItemAlertTimeValidationTest()
        {
            AlertItem item = new AlertItem();

            Assert.IsNotNull(item.AlertTime);
            Assert.AreEqual("", item.Validate("AlertTime"));

            item.AlertTime = DateTime.Now;
            Assert.AreEqual("", item.Validate("AlertTime"));
        }

        [TestMethod]
        public void AlertItemNoticeValidationTest()
        {
            AlertItem item = new AlertItem();

            Assert.IsNull(item.Notice);
            Assert.AreNotEqual("", item.Validate("Notice"));

            item.Notice = "Test Alert";
            Assert.AreEqual("", item.Validate("Notice"));
        }
    }
}
