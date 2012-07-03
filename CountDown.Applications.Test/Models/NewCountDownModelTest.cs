using BigEgg.Framework.Foundation;
using BigEgg.Framework.UnitTesting;
using CountDown.Applications.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountDown.Applications.Test.Models
{
    [TestClass]
    public class NewCountDownModelTest
    {
        [TestMethod]
        public void GeneralNewCountDownModelTest()
        {
            NewCountDownModel item = new NewCountDownModel();
            item.Days = 0;
            item.Hours = 0;
            item.Minutes = 0;
            item.NoticeBranch = "Test Alert";
            item.Notice = "1";
            item.BeforeAlertMinutes = 5;

            Assert.AreEqual("", item.Validate());
        }

        [TestMethod]
        public void PropertiesWithNotification()
        {
            NewCountDownModel item = new NewCountDownModel();

            AssertHelper.PropertyChangedEvent(item, x => x.Days, () => item.Days = 1);
            Assert.AreEqual(1, item.Days);

            AssertHelper.PropertyChangedEvent(item, x => x.Hours, () => item.Hours = 1);
            Assert.AreEqual(1, item.Hours);

            AssertHelper.PropertyChangedEvent(item, x => x.Minutes, () => item.Minutes = 1);
            Assert.AreEqual(1, item.Minutes);

            AssertHelper.PropertyChangedEvent(item, x => x.NoticeBranch, () => item.NoticeBranch = "Test Alert");
            Assert.AreEqual("Test Alert", item.NoticeBranch);

            AssertHelper.PropertyChangedEvent(item, x => x.Notice, () => item.Notice = "1");
            Assert.AreEqual("1", item.Notice);

            AssertHelper.PropertyChangedEvent(item, x => x.BeforeAlertMinutes, () => item.BeforeAlertMinutes = 2);
            Assert.AreEqual(2, item.BeforeAlertMinutes);
        }

        [TestMethod]
        public void NewCountDownModelDaysValidationTest()
        {
            NewCountDownModel item = new NewCountDownModel();
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
            NewCountDownModel item = new NewCountDownModel();
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
            NewCountDownModel item = new NewCountDownModel();
            Assert.AreEqual(0, item.Minutes);
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
            NewCountDownModel item = new NewCountDownModel();
            Assert.AreEqual("", item.NoticeBranch);
            Assert.AreNotEqual("", item.Validate("NoticeBranch"));

            item.NoticeBranch = "Test Alert";
            Assert.AreEqual("Test Alert", item.NoticeBranch);
            Assert.AreEqual("", item.Validate("NoticeBranch"));
        }

        [TestMethod]
        public void NewCountDownModelBeforeAlertMinutesValidationTest()
        {
            NewCountDownModel item = new NewCountDownModel();
            Assert.AreEqual(1, item.BeforeAlertMinutes);
            Assert.AreEqual("", item.Validate("BeforeAlertMinutes"));

            item.BeforeAlertMinutes = 29;
            Assert.AreEqual(29, item.BeforeAlertMinutes);
            Assert.AreEqual("", item.Validate("BeforeAlertMinutes"));

            item.BeforeAlertMinutes = 65535;
            Assert.AreEqual(65535, item.BeforeAlertMinutes);
            Assert.AreEqual("", item.Validate("BeforeAlertMinutes"));

            item.BeforeAlertMinutes = -0;
            Assert.AreEqual(-0, item.BeforeAlertMinutes);
            Assert.AreNotEqual("", item.Validate("BeforeAlertMinutes"));

            item.BeforeAlertMinutes = -1;
            Assert.AreEqual(-1, item.BeforeAlertMinutes);
            Assert.AreNotEqual("", item.Validate("BeforeAlertMinutes"));

            item.BeforeAlertMinutes = 65536;
            Assert.AreEqual(65536, item.BeforeAlertMinutes);
            Assert.AreNotEqual("", item.Validate("BeforeAlertMinutes"));
        }
    }
}
