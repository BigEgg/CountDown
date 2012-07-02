using System.ComponentModel.Composition;
using CountDown.Applications.Services;

namespace CountDown.Applications.Test.Services
{
    [Export(typeof(IPresentationService)), Export]
    public class MockPresentationService : IPresentationService
    {
        public bool InitializeCulturesCalled { get; set; }

        public double VirtualScreenWidth { get; set; }

        public double VirtualScreenHeight { get; set; }


        public void InitializeCultures()
        {
            InitializeCulturesCalled = true;
        }
    }
}
