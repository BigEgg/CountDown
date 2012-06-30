using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BigEgg.Framework.Foundation;
using CountDown.Application.Properties;

namespace CountDown.Application.Domain
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
        private DateTime alartTime;
        private string notice;
        private bool hasAlart = false;
        #endregion

        public CountDownItem()
        {
            this.dataErrorInfoSupport = new DataErrorInfoSupport(this);
        }


        #region Properties
        [Required(ErrorMessageResourceName = "AlartTimeMandatory", ErrorMessageResourceType = typeof(Resources))]
        public DateTime AlartTime 
        {
            get { return this.alartTime; }
            set
            {
                if (this.alartTime != value)
                {
                    this.alartTime = value;
                    RaisePropertyChanged("AlartTime");
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

        public bool HasAlart
        {
            get { return this.hasAlart; }
            set
            {
                if (this.hasAlart != value)
                {
                    this.hasAlart = value;
                    RaisePropertyChanged("HasAlart");
                }
            }
        }
        #endregion
    }
}
