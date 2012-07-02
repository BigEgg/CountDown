using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BigEgg.Framework.Foundation;
using CountDown.Applications.Properties;

namespace CountDown.Applications.Domain
{
    public class CountDownItem : Model, IDataErrorInfo, ICountDownItem
    {
        #region DataErrorInfo
        [NonSerialized]
        private readonly DataErrorInfoSupport dataErrorInfoSupport;

        string IDataErrorInfo.Error { get { return this.dataErrorInfoSupport.Error; } }

        string IDataErrorInfo.this[string memberName] { get { return this.dataErrorInfoSupport[memberName]; } }
        #endregion

        #region Private Memebers
        private DateTime time;
        private DateTime alertTime;
        private string notice;
        private bool hasAlert = false;
        #endregion

        public CountDownItem()
        {
            this.dataErrorInfoSupport = new DataErrorInfoSupport(this);
        }


        #region Properties
        [Required(ErrorMessageResourceName = "AlertTimeMandatory", ErrorMessageResourceType = typeof(Resources))]
        public DateTime AlertTime 
        {
            get { return this.alertTime; }
            set
            {
                if (this.alertTime != value)
                {
                    this.alertTime = value;
                    RaisePropertyChanged("AlertTime");
                }
            }
        }

        [Required(ErrorMessageResourceName = "TimeMandatory", ErrorMessageResourceType = typeof(Resources))]
        public DateTime Time
        {
            get { return this.time; }
            set
            {
                if (this.time != value)
                {
                    this.time = value;
                    RaisePropertyChanged("Time");
                }
            }
        }

        [Required(ErrorMessageResourceName = "NoticeMandatory", ErrorMessageResourceType = typeof(Resources))]
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

        public bool HasAlert
        {
            get { return this.hasAlert; }
            set
            {
                if (this.hasAlert != value)
                {
                    this.hasAlert = value;
                    RaisePropertyChanged("HasAlert");
                }
            }
        }
        #endregion
    }
}
