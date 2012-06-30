using System;
using BigEgg.Framework.Foundation;
using BigEgg.Framework.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigEgg.Framework.Test.Foundation
{
    [TestClass]
    public class PathExtensionsTest
    {
        [TestMethod()]
        public void GetRelativePath_Test()
        {
            string relativePath = PathExtensions.GetRelativePath(@"D:\Windows\Web\Wallpaper\", @"D:\Windows\regedit.exe");
            Assert.AreEqual(@"..\..\regedit.exe", relativePath);

            AssertHelper.ExpectedException<ArgumentNullException>(() => PathExtensions.GetRelativePath(null, null));
            AssertHelper.ExpectedException<ArgumentNullException>(() => PathExtensions.GetRelativePath(null, @"D:\Windows\regedit.exe"));
            AssertHelper.ExpectedException<ArgumentNullException>(() => PathExtensions.GetRelativePath(@"D:\Windows\Web\Wallpaper\", null));

            AssertHelper.ExpectedException<ArgumentException>(() => PathExtensions.GetRelativePath(@"dows\Web\Wallpaper\", @"indows\regedit.exe"));
            AssertHelper.ExpectedException<ArgumentException>(() => PathExtensions.GetRelativePath(@"dows\Web\Wallpaper\", @"regedit.exe"));
            AssertHelper.ExpectedException<ArgumentException>(() => PathExtensions.GetRelativePath(@"D:\Windows\Web\Wallpaper\", @"ndows\regedit.exe"));
        }
    }
}
