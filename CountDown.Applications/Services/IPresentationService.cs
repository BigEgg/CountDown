namespace CountDown.Applications.Services
{
    public interface IPresentationService
    {
        double VirtualScreenWidth { get; }

        double VirtualScreenHeight { get; }


        void InitializeCultures();
    }
}
