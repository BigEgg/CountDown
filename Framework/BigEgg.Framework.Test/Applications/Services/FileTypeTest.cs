using System;
using BigEgg.Framework.Applications.Services;
using BigEgg.Framework.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigEgg.Framework.Test.Applications.Services
{
    [TestClass]
    public class FileTypeTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            FileType fileType = new FileType("RichText Documents (*.rtf)", ".rtf");
            Assert.AreEqual("RichText Documents (*.rtf)", fileType.Description);
            Assert.AreEqual(".rtf", fileType.FileExtension);

            AssertHelper.ExpectedException<ArgumentException>(() => new FileType(null, ".rtf"));
            AssertHelper.ExpectedException<ArgumentException>(() => new FileType("", ".rtf"));
            AssertHelper.ExpectedException<ArgumentException>(() => new FileType("RichText Documents", null));
            AssertHelper.ExpectedException<ArgumentException>(() => new FileType("RichText Documents", ""));
            AssertHelper.ExpectedException<ArgumentException>(() => new FileType("RichText Documents", "rtf"));
        }
    }
}
