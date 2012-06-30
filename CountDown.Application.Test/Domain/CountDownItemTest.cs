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
            item.AlartTime = DateTime.Now.AddHours(1);
            item.Notice = "Test Alart";
            item.HasAlart = false;

            Assert.AreEqual("", item.Validate());
        }

        [TestMethod]
        public void PropertiesWithNotification()
        {
            DateTime now = DateTime.Now;

            CountDownItem item = new CountDownItem();
            Assert.AreEqual(false, item.HasAlart);

            AssertHelper.PropertyChangedEvent(item, x => x.Time, () => item.Time = now);
            Assert.AreEqual(now, item.Time);

            AssertHelper.PropertyChangedEvent(item, x => x.AlartTime, () => item.AlartTime = now.AddHours(1));
            Assert.AreEqual(now.AddHours(1), item.AlartTime);
           
            AssertHelper.PropertyChangedEvent(item, x => x.Notice, () => item.Notice = "Test Alart");
            Assert.AreEqual("Test Alart", item.Notice);

            item.HasAlart = true;
            Assert.AreEqual(true, item.HasAlart);
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
        public void CountDownItemAlartTimeValidationTest()
        {
            CountDownItem item = new CountDownItem();

            Assert.IsNotNull(item.AlartTime);
            Assert.AreEqual("", item.Validate("AlartTime"));

            item.AlartTime = DateTime.Now;
            Assert.AreEqual("", item.Validate("AlartTime"));
        }

        [TestMethod]
        public void CountDownItemNoticeValidationTest()
        {
            CountDownItem item = new CountDownItem();

            Assert.IsNull(item.Notice);
            Assert.AreNotEqual("", item.Validate("Notice"));

            item.Notice = "Test Alart";
            Assert.AreEqual("", item.Validate("Notice"));
        }
    }
}
