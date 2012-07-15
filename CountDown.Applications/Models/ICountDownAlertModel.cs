namespace CountDown.Applications.Models
{
    public interface ICountDownAlertModel
    {
        int Days { get; set; }
        int Hours { get; set; }
        int Minutes { get; set; }
    }
}
