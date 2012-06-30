using BigEgg.Framework.Applications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigEgg.Framework.Test.Applications
{
    [TestClass]
    public class ApplicationInfoTest
    {
        public TestContext TestContext { get; set; }


        [TestMethod]
        public void ApplicationInfoPropertiesTest()
        {
            // This doesn't work because the Visual Studio Unit Testing Framework
            // doesn't have an entry assembly

            string productName = ApplicationInfo.ProductName;
            string version = ApplicationInfo.Version;
            string company = ApplicationInfo.Company;
            string copyright = ApplicationInfo.Copyright;
            string applicationPath = ApplicationInfo.ApplicationPath;

            // The second time it returns the cached values

            productName = ApplicationInfo.ProductName;
            version = ApplicationInfo.Version;
            company = ApplicationInfo.Company;
            copyright = ApplicationInfo.Copyright;
            applicationPath = ApplicationInfo.ApplicationPath;
        }
    }
}
