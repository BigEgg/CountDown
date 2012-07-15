using BigEgg.Framework.Foundation;
using BigEgg.Framework.UnitTesting;
using CountDown.Applications.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountDown.Applications.Test.Models
{
    [TestClass]
    public class CountDownAlertModelTest
    {
        [TestMethod]
        public void GeneralNewCountDownModelTest()
        {
            CountDownAlertModel item = new CountDownAlertModel();
            item.Days = 0;
            item.Hours = 0;
            item.Minutes = 0;
            item.NoticeBranch = "Test Alert";
            item.Notice = "1";

            Assert.AreEqual("", item.Validate());
        }

        [TestMethod]
        public void PropertiesWithNotification()
        {
            CountDownAlertModel item = new CountDownAlertModel();

            AssertHelper.PropertyChangedEvent(item, x => x.Days, () => item.Days = 1);
            Assert.AreEqual(1, item.Days);

            AssertHelper.PropertyChangedEvent(item, x => x.Hours, () => item.Hours = 1);
            Assert.AreEqual(1, item.Hours);

            AssertHelper.PropertyChangedEvent(item, x => x.Minutes, () => item.Minutes = 5);
            Assert.AreEqual(5, item.Minutes);

            AssertHelper.PropertyChangedEvent(item, x => x.NoticeBranch, () => item.NoticeBranch = "Test Alert");
            Assert.AreEqual("Test Alert", item.NoticeBranch);

            AssertHelper.PropertyChangedEvent(item, x => x.Notice, () => item.Notice = "1");
            Assert.AreEqual("1", item.Notice);
        }

        [TestMethod]
        public void NewCountDownModelDaysValidationTest()
        {
            CountDownAlertModel item = new CountDownAlertModel();
            Assert.AreEqual(0, item.Days);
            Assert.AreEqual("", item.Validate("Days"));

            item.Days = 100;
            Assert.AreEqual(100, item.Days);
            Assert.AreEqual("", item.Validate("Days"));

            item.Days = 365;
            Assert.AreEqual(365, item.Days);
            Assert.AreEqual("", item.Validate("Days"));

            item.Days = -0;
            Assert.AreEqual(-0, item.Days);
            Assert.AreEqual("", item.Validate("Days"));

            item.Days = -1;
            Assert.AreEqual(-1, item.Days);
            Assert.AreNotEqual("", item.Validate("Days"));

            item.Days = 366;
            Assert.AreEqual(366, item.Days);
            Assert.AreNotEqual("", item.Validate("Days"));
        }

        [TestMethod]
        public void NewCountDownModelHoursValidationTest()
        {
            CountDownAlertModel item = new CountDownAlertModel();
            Assert.AreEqual(0, item.Hours);
            Assert.AreEqual("", item.Validate("Hours"));

            item.Hours = 10;
            Assert.AreEqual(10, item.Hours);
            Assert.AreEqual("", item.Validate("Hours"));

            item.Hours = 23;
            Assert.AreEqual(23, item.Hours);
            Assert.AreEqual("", item.Validate("Hours"));

            item.Hours = -0;
            Assert.AreEqual(-0, item.Hours);
            Assert.AreEqual("", item.Validate("Hours"));

            item.Hours = -1;
            Assert.AreEqual(-1, item.Hours);
            Assert.AreNotEqual("", item.Validate("Hours"));

            item.Hours = 24;
            Assert.AreEqual(24, item.Hours);
            Assert.AreNotEqual("", item.Validate("Hours"));
        }

        [TestMethod]
        public void NewCountDownModelMinutesValidationTest()
        {
            CountDownAlertModel item = new CountDownAlertModel();
            Assert.AreEqual(1, item.Minutes);
            Assert.AreEqual("", item.Validate("Minutes"));

            item.Minutes = 29;
            Assert.AreEqual(29, item.Minutes);
            Assert.AreEqual("", item.Validate("Minutes"));

            item.Minutes = 59;
            Assert.AreEqual(59, item.Minutes);
            Assert.AreEqual("", item.Validate("Minutes"));

            item.Minutes = -0;
            Assert.AreEqual(-0, item.Minutes);
            Assert.AreEqual("", item.Validate("Minutes"));

            item.Minutes = -1;
            Assert.AreEqual(-1, item.Minutes);
            Assert.AreNotEqual("", item.Validate("Minutes"));

            item.Minutes = 60;
            Assert.AreEqual(60, item.Minutes);
            Assert.AreNotEqual("", item.Validate("Minutes"));
        }

        [TestMethod]
        public void NewCountDownModelNoticeBranchValidationTest()
        {
            CountDownAlertModel item = new CountDownAlertModel();
            Assert.AreEqual(string.Empty, item.NoticeBranch);
            Assert.AreNotEqual(string.Empty, item.Validate("NoticeBranch"));

            item.NoticeBranch = "Test Alert";
            Assert.AreEqual("Test Alert", item.NoticeBranch);
            Assert.AreEqual("", item.Validate("NoticeBranch"));
        }
    }
}
