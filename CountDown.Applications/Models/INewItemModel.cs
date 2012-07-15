using System;
using CountDown.Applications.Domain;

namespace CountDown.Applications.Models
{
    public interface INewItemModel
    {
        DateTime Time { get; set; }

        string NoticeBranch { get; set; }

        string Notice { get; set; }

        AlertItem ToAlertItem(int beforeAlertMinutes);

        void Clean();
    }
}
