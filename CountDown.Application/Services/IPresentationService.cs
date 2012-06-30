﻿namespace CountDown.Application.Services
{
    public interface IPresentationService
    {
        double VirtualScreenWidth { get; }

        double VirtualScreenHeight { get; }


        void InitializeCultures();
    }
}
