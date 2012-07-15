using System;
using System.ComponentModel.DataAnnotations;
using CountDown.Applications.Properties;
using CountDown.Applications.Domain;

namespace CountDown.Applications.Models
{
    public class AlertAtTimeModel : NewItemModelBase
    {
        #region Private Members
        private DateTime date;
        private int hours;
        private int minutes;
        #endregion

        public AlertAtTimeModel()
            : base()
        {
            DateTime alertDate = DateTime.Now.AddHours(1);

            this.date = alertDate.Date;
            this.hours = alertDate.Hour;
            this.minutes = alertDate.Minute;
        }


        #region Properties
        [Required(ErrorMessageResourceName = "DateMandatory", ErrorMessageResourceType = typeof(Resources))]
        public DateTime Date
        {
            get { return this.date; }
            set
            {
                if (this.date != value)
                {
                    this.date = value;
                    RaisePropertyChanged("Date");
                }
            }
        }

        [Required(ErrorMessageResourceName = "HoursMandatory", ErrorMessageResourceType = typeof(Resources))]
        [Range(0, 23, ErrorMessageResourceName = "HoursRange", ErrorMessageResourceType = typeof(Resources))]
        public int Hours
        {
            get { return this.hours; }
            set
            {
                if (this.hours != value)
                {
                    this.hours = value;
                    RaisePropertyChanged("Hours");
                }
            }
        }

        [Required(ErrorMessageResourceName = "MinutesMandatory", ErrorMessageResourceType = typeof(Resources))]
        [Range(0, 59, ErrorMessageResourceName = "MinutesRange", ErrorMessageResourceType = typeof(Resources))]
        public int Minutes
        {
            get { return this.minutes; }
            set
            {
                if (this.minutes != value)
                {
                    this.minutes = value;
                    RaisePropertyChanged("Minutes");
                }
            }
        }
        #endregion

        public override AlertItem ToAlertItem(int beforeAlertMinutes)
        {
            AlertItem item = new AlertItem();
            item.Time = this.date.AddHours(this.hours).AddMinutes(this.minutes);
            item.HasAlert = false;

            if (string.IsNullOrWhiteSpace(this.Notice))
                item.Notice = string.Format(Resources.OnlyNoticeBranch, this.NoticeBranch);
            else
                item.Notice = string.Format(Resources.NoticeFormat, this.NoticeBranch, this.Notice);

            item.AlertTime = item.Time.AddMinutes(0 - beforeAlertMinutes);

            return item;
        }

        public override void Clean()
        {
            base.Clean();
            DateTime alertDate = DateTime.Now.AddHours(1);

            this.date = alertDate.Date;
            this.hours = alertDate.Hour;
            this.minutes = alertDate.Minute;
        }
    }
}
