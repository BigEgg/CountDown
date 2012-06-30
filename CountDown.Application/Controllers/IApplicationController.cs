using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountDown.Application.Controllers
{
    /// <summary>
    /// Responsible for the application lifecycle.
    /// </summary>
    public interface IApplicationController
    {
        void Initialize();

        void Run();

        void Shutdown();
    }
}
