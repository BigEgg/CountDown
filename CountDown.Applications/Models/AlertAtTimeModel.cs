using System;

namespace CountDown.Applications.Models
{
    public class AlertAtTimeModel : NewItemModelBase
    {
        public AlertAtTimeModel()
            : base()
        {
            this.Time = this.Time.AddHours(1);
        }

        public override void Clean()
        {
            base.Clean();
            this.Time = DateTime.Now.AddHours(1);
        }
    }
}
