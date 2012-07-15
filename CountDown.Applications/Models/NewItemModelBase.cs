using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BigEgg.Framework.Foundation;
using CountDown.Applications.Domain;
using CountDown.Applications.Properties;

namespace CountDown.Applications.Models
{
    public class NewItemModelBase : Model, IDataErrorInfo, INewItemModel
    {
        #region DataErrorInfo
        [NonSerialized]
        private readonly DataErrorInfoSupport dataErrorInfoSupport;

        string IDataErrorInfo.Error { get { return this.dataErrorInfoSupport.Error; } }

        string IDataErrorInfo.this[string memberName] { get { return this.dataErrorInfoSupport[memberName]; } }
        #endregion

        #region Private Members
        private DateTime time;
        private string noticeBranch;
        private string notice;
        #endregion

        public NewItemModelBase()
        {
            this.dataErrorInfoSupport = new DataErrorInfoSupport(this);

            this.time = DateTime.Now;
            this.noticeBranch = string.Empty;
            this.notice = string.Empty;
        }

        #region Properties
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

        [Required(ErrorMessageResourceName = "NoticeBranchMandatory", ErrorMessageResourceType = typeof(Resources))]
        public string NoticeBranch
        {
            get { return this.noticeBranch; }
            set
            {
                if (value == null)
                    return;

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
        #endregion

        public virtual AlertItem ToAlertItem(int beforeAlertMinutes)
        {
            AlertItem item = new AlertItem
            {
                Time = this.Time,
                AlertTime = this.Time.AddMinutes(0 - beforeAlertMinutes),
                Notice = string.Empty,
                HasAlert = false
            };

            if (string.IsNullOrWhiteSpace(this.Notice))
                this.Notice = string.Format(Resources.OnlyNoticeBranch, this.NoticeBranch);
            else
                this.Notice = string.Format(Resources.NoticeFormat, this.NoticeBranch, this.Notice);

            return item;
        }

        public virtual void Clean()
        {
            this.Time = DateTime.Now;
            this.Notice = string.Empty;
            this.NoticeBranch = string.Empty;
        }
    }
}
