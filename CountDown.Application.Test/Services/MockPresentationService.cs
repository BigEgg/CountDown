using System.ComponentModel.Composition;
using CountDown.Application.Services;

namespace CountDown.Application.Test.Services
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
