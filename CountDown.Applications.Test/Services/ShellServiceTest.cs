using BigEgg.Framework.UnitTesting;
using CountDown.Applications.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountDown.Applications.Test.Services
{
    [TestClass]
    public class ShellServiceTest
    {
        [TestMethod]
        public void SetViewTest()
        {
            ShellService shellService = new ShellService();
            object mockView = new object();

            AssertHelper.PropertyChangedEvent(shellService, x => x.ShellView, () =>
                shellService.ShellView = mockView);
            Assert.AreEqual(mockView, shellService.ShellView);
        }

    }
}
