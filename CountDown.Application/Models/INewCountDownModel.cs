﻿namespace CountDown.Application.Models
{
    public interface INewCountDownModel
    {
        int Days { get; set; }
        int Hours { get; set; }
        int Minutes { get; set; }

        string NoticeBranch { get; set; }
        string Notice { get; set; }

        int BeforeAlartMinutes { get; set; }
    }
}
