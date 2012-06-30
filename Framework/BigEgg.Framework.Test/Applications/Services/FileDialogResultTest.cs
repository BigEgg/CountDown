using BigEgg.Framework.Applications.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigEgg.Framework.Test.Applications.Services
{
    [TestClass]
    public class FileDialogResultTest
    {
        [TestMethod]
        public void ResultTest()
        {
            FileDialogResult result = new FileDialogResult();
            Assert.IsFalse(result.IsValid);

            FileType rtfFileType = new FileType("RichText Document", ".rtf");
            result = new FileDialogResult(@"C:\Document 1.rtf", rtfFileType);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(@"C:\Document 1.rtf", result.FileName);
            Assert.AreEqual(rtfFileType, result.SelectedFileType);
        }
    }
}
