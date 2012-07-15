using System;
using System.Linq;
using BigEgg.Framework.UnitTesting;
using CountDown.Applications.Domain;
using CountDown.Applications.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountDown.Applications.Test.Services
{
    [TestClass]
    public class DataServiceTest : TestClassBase
    {
        [TestMethod]
        public void GeneralDataServiceTest()
        {
            IDataService dataService = Container.GetExportedValue<IDataService>();

            Assert.AreEqual(0, dataService.Items.Count);
            Assert.AreEqual(0, dataService.SelectItems.Count);
            Assert.AreEqual(0, dataService.AlertedItems.Count);

            dataService.Items.Add(
                new AlertItem
                {
                    Time = DateTime.Now,
                    Notice = "Test 1",
                    AlertTime = DateTime.Now.AddHours(1)
                }
            );

            Assert.AreEqual(1, dataService.Items.Count);
            Assert.AreEqual(1, dataService.SelectItems.Count);
            Assert.AreEqual(0, dataService.AlertedItems.Count);

            dataService.Items.Add(new AlertItem
                {
                    Time = DateTime.Now,
                    Notice = "Test 2",
                    AlertTime = DateTime.Now.AddHours(1)
                }
            );

            Assert.AreEqual(2, dataService.Items.Count);
            Assert.AreEqual(1, dataService.SelectItems.Count);
            Assert.AreEqual("Test 1", dataService.SelectItems.First().Notice);
            Assert.AreEqual(0, dataService.AlertedItems.Count);

            dataService.AlertedItems.Add(
                new AlertItem
                {
                    Time = DateTime.Now,
                    Notice = "Test 3",
                    AlertTime = DateTime.Now.AddHours(1)
                }
            );

            Assert.AreEqual(2, dataService.Items.Count);
            Assert.AreEqual(1, dataService.SelectItems.Count);
            Assert.AreEqual("Test 1", dataService.SelectItems.First().Notice);
            Assert.AreEqual(1, dataService.AlertedItems.Count);
        }

        [TestMethod]
        public void PropertiesWithNotification()
        {
            IDataService dataService = Container.GetExportedValue<IDataService>();

            AssertHelper.PropertyChangedEvent(dataService, x => x.AlertedItems, () => 
                dataService.AlertedItems.Add(
                    new AlertItem
                    {
                        Time = DateTime.Now,
                        Notice = "Test 3",
                        AlertTime = DateTime.Now.AddHours(1)
                    }
                )
            );
            Assert.AreEqual(1, dataService.AlertedItems.Count);
            Assert.AreEqual("Test 3", dataService.AlertedItems.First().Notice);

            AssertHelper.PropertyChangedEvent(dataService, x => x.SelectItems, () =>
                dataService.Items.Add(
                    new AlertItem
                    {
                        Time = DateTime.Now,
                        Notice = "Test 1",
                        AlertTime = DateTime.Now.AddHours(1)
                    }
                )
            );

            AssertHelper.PropertyChangedEvent(dataService, x => x.SelectItems, () => dataService.SelectItems.Clear());
        }
    }
}
