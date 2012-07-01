using System;
using BigEgg.Framework.Foundation;
using BigEgg.Framework.UnitTesting;
using CountDown.Application.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountDown.Application.Test.Domain
{
    [TestClass]
    public class CountDownItemTest
    {
        [TestMethod]
        public void GeneralCountDownItemTest()
        {
            CountDownItem item = new CountDownItem();
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

            CountDownItem item = new CountDownItem();
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
        public void CountDownItemTimeValidationTest()
        {
            CountDownItem item = new CountDownItem();

            Assert.IsNotNull(item.Time);
            Assert.AreEqual("", item.Validate("Time"));

            item.Time = DateTime.Now;
            Assert.AreEqual("", item.Validate("Time"));
        }

        [TestMethod]
        public void CountDownItemAlertTimeValidationTest()
        {
            CountDownItem item = new CountDownItem();

            Assert.IsNotNull(item.AlertTime);
            Assert.AreEqual("", item.Validate("AlertTime"));

            item.AlertTime = DateTime.Now;
            Assert.AreEqual("", item.Validate("AlertTime"));
        }

        [TestMethod]
        public void CountDownItemNoticeValidationTest()
        {
            CountDownItem item = new CountDownItem();

            Assert.IsNull(item.Notice);
            Assert.AreNotEqual("", item.Validate("Notice"));

            item.Notice = "Test Alert";
            Assert.AreEqual("", item.Validate("Notice"));
        }
    }
}
