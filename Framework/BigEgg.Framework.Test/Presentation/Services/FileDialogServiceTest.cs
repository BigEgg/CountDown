﻿using System;
using System.Collections.Generic;
using System.Reflection;
using BigEgg.Framework.Applications.Services;
using BigEgg.Framework.Presentation.Services;
using BigEgg.Framework.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigEgg.Framework.Test.Presentation.Services
{
    [TestClass]
    public class FileDialogServiceTest
    {
        [TestMethod]
        public void ShowOpenFileDialogTest()
        {
            FileDialogService service = new FileDialogService();
            
            AssertHelper.ExpectedException<ArgumentNullException>(() => service.ShowOpenFileDialog(null, null, null));
            AssertHelper.ExpectedException<ArgumentException>(() => 
                service.ShowOpenFileDialog(new FileType[] {}, null, null));
        }

        [TestMethod]
        public void ShowSaveFileDialogTest()
        {
            FileDialogService service = new FileDialogService();

            AssertHelper.ExpectedException<ArgumentNullException>(() => service.ShowSaveFileDialog(null, null, null));
            AssertHelper.ExpectedException<ArgumentException>(() =>
                service.ShowSaveFileDialog(new FileType[] { }, null, null));
        }

        [TestMethod]
        public void CreateFilterTest()
        {
            FileType rtfFileType = new FileType("RichText Document", ".rtf");
            FileType xpsFileType = new FileType("XPS Document", ".xps");
            
            
            Assert.AreEqual("RichText Document|*.rtf", InvokeCreateFilter(new FileType[] { rtfFileType }));
            Assert.AreEqual("RichText Document|*.rtf|XPS Document|*.xps",
                InvokeCreateFilter(new FileType[] { rtfFileType, xpsFileType }));
        }

        private static string InvokeCreateFilter(IEnumerable<FileType> fileTypes)
        {
            MethodInfo createFilterInfo = typeof(FileDialogService).GetMethod("CreateFilter",
                BindingFlags.Static | BindingFlags.NonPublic);
            return (string)createFilterInfo.Invoke(null, new object[] { fileTypes });
        }
    }
}
