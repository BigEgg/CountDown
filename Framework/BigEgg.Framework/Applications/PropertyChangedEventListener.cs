using System;
using System.ComponentModel;
using System.Windows;

namespace BigEgg.Framework.Applications
{
    internal class PropertyChangedEventListener : IWeakEventListener
    {
        private readonly INotifyPropertyChanged source;
        private readonly PropertyChangedEventHandler handler;


        public PropertyChangedEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
        {
            if (source == null) { throw new ArgumentNullException("source"); }
            if (handler == null) { throw new ArgumentNullException("handler"); }
            this.source = source;
            this.handler = handler;
        }


        public INotifyPropertyChanged Source { get { return this.source; } }

        public PropertyChangedEventHandler Handler { get { return this.handler; } }


        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            this.handler(sender, (PropertyChangedEventArgs)e);
            return true;
        }
    }
}
