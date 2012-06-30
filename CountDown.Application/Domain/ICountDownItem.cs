using System;

namespace CountDown.Application.Domain
{
    public interface ICountDownItem
    {
        DateTime AlartTime { get; set; }

        DateTime Time { get; set; }

        string Notice { get; set; }

        bool HasAlart { get; set; }
    }
}
