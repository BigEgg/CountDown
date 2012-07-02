namespace CountDown.Applications.Models
{
    public interface INewCountDownModel
    {
        int Days { get; set; }
        int Hours { get; set; }
        int Minutes { get; set; }

        string NoticeBranch { get; set; }
        string Notice { get; set; }

        int BeforeAlertMinutes { get; set; }
    }
}
