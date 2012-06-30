using System;
using System.Collections.Specialized;
using System.Windows;

namespace BigEgg.Framework.Applications
{
    internal class CollectionChangedEventListener : IWeakEventListener
    {
        private readonly INotifyCollectionChanged source;
        private readonly NotifyCollectionChangedEventHandler handler;


        public CollectionChangedEventListener(INotifyCollectionChanged source, NotifyCollectionChangedEventHandler handler)
        {
            if (source == null) { throw new ArgumentNullException("source"); }
            if (handler == null) { throw new ArgumentNullException("handler"); }
            this.source = source;
            this.handler = handler;
        }


        public INotifyCollectionChanged Source { get { return this.source; } }

        public NotifyCollectionChangedEventHandler Handler { get { return this.handler; } }


        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            this.handler(sender, (NotifyCollectionChangedEventArgs)e);
            return true;
        }
    }
}
