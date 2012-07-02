using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BigEgg.Framework.Foundation;
using CountDown.Applications.Properties;

namespace CountDown.Applications.Models
{
    public class NewCountDownModel : Model, IDataErrorInfo, INewCountDownModel
    {
        #region DataErrorInfo
        [NonSerialized]
        private readonly DataErrorInfoSupport dataErrorInfoSupport;

        string IDataErrorInfo.Error { get { return this.dataErrorInfoSupport.Error; } }

        string IDataErrorInfo.this[string memberName] { get { return this.dataErrorInfoSupport[memberName]; } }
        #endregion

        #region Private Members
        private int days;
        private int hours;
        private int minutes;

        private string noticeBranch;
        private string notice;

        private int beforeAlertMinutes;
        #endregion

        public NewCountDownModel()
        {
            this.dataErrorInfoSupport = new DataErrorInfoSupport(this);

            this.Days = 0;
            this.Hours = 0;
            this.Minutes = 0;
            this.NoticeBranch = string.Empty;
            this.Notice = string.Empty;
            this.BeforeAlertMinutes = Settings.Default.DefautBeforeAlertMinutes;
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

        [Required(ErrorMessageResourceName = "NoticeBranchMandatory", ErrorMessageResourceType = typeof(Resources))]
        public string NoticeBranch
        {
            get { return this.noticeBranch; }
            set
            {
                if (this.noticeBranch != value)
                {
                    this.noticeBranch = value;
                    RaisePropertyChanged("NoticeBranch");
                }
            }
        }
        public string Notice
        {
            get { return this.notice; }
            set
            {
                if (this.notice != value)
                {
                    this.notice = value;
                    RaisePropertyChanged("Notice");
                }
            }
        }

        [Required(ErrorMessageResourceName = "BeforeAlertMinutesMandatory", ErrorMessageResourceType = typeof(Resources))]
        [Range(1, 65535, ErrorMessageResourceName = "BeforeAlertMinutesRange", ErrorMessageResourceType = typeof(Resources))]
        public int BeforeAlertMinutes
        {
            get { return this.beforeAlertMinutes; }
            set
            {
                if (this.beforeAlertMinutes != value)
                {
                    this.beforeAlertMinutes = value;
                    RaisePropertyChanged("BeforeAlertMinutes");
                }
            }
        }
        #endregion

    }
}
