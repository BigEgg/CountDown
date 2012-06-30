using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CountDown.Application.Models;
using BigEgg.Framework.Foundation;
using BigEgg.Framework.UnitTesting;

namespace CountDown.Application.Test.Models
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
            item.NoticeBranch = "Test Alart";
            item.Notice = "1";
            item.BeforeAlartMinutes = 5;

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

            AssertHelper.PropertyChangedEvent(item, x => x.NoticeBranch, () => item.NoticeBranch = "Test Alart");
            Assert.AreEqual("Test Alart", item.NoticeBranch);

            AssertHelper.PropertyChangedEvent(item, x => x.Notice, () => item.Notice = "1");
            Assert.AreEqual("1", item.Notice);

            AssertHelper.PropertyChangedEvent(item, x => x.BeforeAlartMinutes, () => item.BeforeAlartMinutes = 2);
            Assert.AreEqual(2, item.BeforeAlartMinutes);
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

            item.NoticeBranch = "Test Alart";
            Assert.AreEqual("Test Alart", item.NoticeBranch);
            Assert.AreEqual("", item.Validate("NoticeBranch"));
        }

        [TestMethod]
        public void NewCountDownModelBeforeAlartMinutesValidationTest()
        {
            NewCountDownModel item = new NewCountDownModel();
            Assert.AreEqual(1, item.BeforeAlartMinutes);
            Assert.AreEqual("", item.Validate("BeforeAlartMinutes"));

            item.BeforeAlartMinutes = 29;
            Assert.AreEqual(29, item.BeforeAlartMinutes);
            Assert.AreEqual("", item.Validate("BeforeAlartMinutes"));

            item.BeforeAlartMinutes = 65535;
            Assert.AreEqual(65535, item.BeforeAlartMinutes);
            Assert.AreEqual("", item.Validate("BeforeAlartMinutes"));

            item.BeforeAlartMinutes = -0;
            Assert.AreEqual(-0, item.BeforeAlartMinutes);
            Assert.AreEqual("", item.Validate("BeforeAlartMinutes"));

            item.BeforeAlartMinutes = -1;
            Assert.AreEqual(-1, item.BeforeAlartMinutes);
            Assert.AreNotEqual("", item.Validate("BeforeAlartMinutes"));

            item.BeforeAlartMinutes = 65536;
            Assert.AreEqual(65536, item.BeforeAlartMinutes);
            Assert.AreNotEqual("", item.Validate("BeforeAlartMinutes"));
        }
    }
}
