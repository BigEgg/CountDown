using System;

namespace CountDown.Applications.Domain
{
    public interface IAlertItem
    {
        DateTime AlertTime { get; set; }

        DateTime Time { get; set; }

        string Notice { get; set; }

        bool HasAlert { get; set; }
    }
}
