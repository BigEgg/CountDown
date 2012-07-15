using System;
using System.ComponentModel.DataAnnotations;
using CountDown.Applications.Domain;
using CountDown.Applications.Properties;

namespace CountDown.Applications.Models
{
    [MetadataType(typeof(NewItemModelBase))]
    public class CountDownAlertModel : NewItemModelBase, ICountDownAlertModel
    {
        #region Private Members
        private int days;
        private int hours;
        private int minutes;
        #endregion

        public CountDownAlertModel()
        {
            this.days = 0;
            this.hours = 0;
            this.minutes = 1;
        }

        #region Properties
        [Required(ErrorMessageResourceName = "DaysMandatory", ErrorMessageResourceType = typeof(Resources))]
        [Range(0, 365, ErrorMessageResourceName = "DaysRange", ErrorMessageResourceType = typeof(Resources))]
        public int Days
        {
            get { return this.days; }
            set 
            {
                if (this.days != value)
                {
                    this.days = value;
                    RaisePropertyChanged("Days");
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
            item.Time = DateTime.Now;
            item.HasAlert = false;

            item.Time = item.Time.Add(new TimeSpan(this.Days, this.Hours, this.Minutes, 0));

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
            this.Days = 0;
            this.Hours = 0;
            this.Minutes = 1;
        }
    }
}
